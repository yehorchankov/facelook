#region

using System;
using System.Linq;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.DAL.Concrete
{
    public class TicketRepository : ITicketRepository
    {
        private readonly EfDbContext _context = new EfDbContext();

        /// <summary>
        ///     Searches DB to find authentication ticket that matches user's id and password's hash
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="hash">Password hash</param>
        /// <returns>Authentication result</returns>
        public bool Authenticate(string nickname, string hash)
        {
            AuthenticationTicket ticket = _context.AuthTicket.FirstOrDefault(m =>
                nickname != null && (m.PasswordHash == hash && m.UserNickname == nickname));

            if (ticket != null)
            {
                ticket.LastAccessTime = DateTime.Now;
                _context.SaveChanges();

                return true;
            }
            return false;
        }

        /// <summary>
        ///     Creates new entry in database about the user
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="hash"></param>
        /// <returns>Ticket guid to confirm</returns>
        public Guid CreateTicket(string nickname, string hash)
        {
            Guid ticketKey = Guid.NewGuid();

            AuthenticationTicket ticket = new AuthenticationTicket
            {
                Id = 0,
                PasswordHash = hash,
                UserNickname = nickname,
                RegistrationDate = DateTime.Now,
                TicketKey = ticketKey
            };
            _context.AuthTicket.Add(ticket);

            _context.SaveChanges();

            return ticketKey;
        }

        /// <summary>
        ///     Removes the ticket from database
        /// </summary>
        /// <param name="nickname">User id</param>
        /// <param name="hash">Password hash</param>
        public void RemoveTicket(string nickname, string hash)
        {
            AuthenticationTicket ticket = _context.AuthTicket.FirstOrDefault(t =>
                t.PasswordHash == hash && t.UserNickname == nickname);

            if (ticket != null) _context.AuthTicket.Remove(ticket);

            _context.SaveChanges();
        }

        /// <summary>
        ///     Provides logic to confirm user's account
        /// </summary>
        /// <param name="nickname"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ConfirmTicket(string nickname, Guid key)
        {
            //Cookies key, DB key and email key must be equal to confirm account
            AuthenticationTicket ticket =
                _context.AuthTicket.FirstOrDefault(t => t.UserNickname == nickname);

            if (ticket == null)
            {
                return false;
            }

            bool result = key == ticket.TicketKey;

            if (!result)
            {
                return false;
            }

            User user = _context.Users.FirstOrDefault(u => u.NickName == nickname);
            user.AccountConfirmed = true;
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        ///     Checks DB to find usages of the <code>nickname</code>
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns>Result of checking (true if the nickname exists)</returns>
        public bool IsNicknameUsed(string nickname)
        {
            return Enumerable.Any(_context.AuthTicket,
                ticket => ticket.UserNickname == nickname);
        }
    }
}