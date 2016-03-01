#region

using System;

#endregion

namespace FaceLook.DAL.Entities
{
    public class AuthenticationTicket
    {
        public int Id { get; set; }
        public string UserNickname { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastAccessTime { get; set; }
        public Guid TicketKey { get; set; }
    }
}