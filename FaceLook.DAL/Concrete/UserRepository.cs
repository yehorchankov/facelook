#region

using System.Collections.Generic;
using System.Linq;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly EfDbContext _context = new EfDbContext();

        public UserRepository()
        {
        }

        public UserRepository(EfDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<User> Users
        {
            get { return _context.Users; }
        }

        /// <summary>
        ///     Save all changed infomation about user an returns ammount of changes saved in database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Ammount of changes in database</returns>
        public int SaveUser(User user)
        {
            if (user.Id == 0)
            {
                //Initialize fields when user object is just created
                user.Friends = new List<User>();
                user.FriendsToConfirm = new List<FriendshipRequest>();
                user.Posts = new List<Post>();

                _context.Users.Add(user);
            }
            else
            {
                User userInDb = _context.Users.FirstOrDefault(u => u.NickName == user.NickName);
                if (userInDb != null)
                {
                    userInDb.AccountConfirmed = user.AccountConfirmed;
                    userInDb.AccountStatus = user.AccountStatus;
                    userInDb.Address = user.Address;
                    userInDb.BirthDate = user.BirthDate;
                    userInDb.Email = user.Email;
                    userInDb.FirstName = user.FirstName;
                    userInDb.Friends = user.Friends;
                    userInDb.FriendsToConfirm = user.FriendsToConfirm;
                    userInDb.Gender = user.Gender;
                    userInDb.Information = user.Information;
                    userInDb.LastName = user.LastName;
                    userInDb.MiddleName = user.MiddleName;
                    userInDb.MimeType = user.MimeType;
                    userInDb.NickName = user.NickName;
                    userInDb.Phone = user.Phone;
                    userInDb.Photo = user.Photo;
                    userInDb.Posts = user.Posts;
                    userInDb.RegistrationDate = user.RegistrationDate;
                }
            }

            return _context.SaveChanges();
        }

        /// <summary>
        ///     Removes user from database and returns the user
        /// </summary>
        /// <param name="Id">User's id</param>
        /// <returns>Deleted user</returns>
        public User DeleteUser(int Id)
        {
            User userInDb = _context.Users.Find(Id);
            if (userInDb != null)
            {
                _context.Users.Remove(userInDb);
                _context.SaveChanges();
            }
            return userInDb;
        }

        public User GetUserById(int Id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == Id);
        }

        public User GetUserByNickname(string nickname)
        {
            User user = _context.Users
                .FirstOrDefault(u => u.NickName == nickname);

            if (user == null)
                return null;
            if (user.Friends == null)
                user.Friends = new List<User>();
            if (user.FriendsToConfirm == null)
                user.FriendsToConfirm = new List<FriendshipRequest>();
            if (user.Posts == null)
                user.Posts = new List<Post>();

            return user;
        }

        public void SaveConversation(Conversation conversation)
        {
            if (conversation.Id == 0)
            {
                conversation.Messages = new List<Message>();

                _context.Conversations.Add(conversation);
            }
            else
            {
                Conversation conv = GetConversation(conversation.User1Nick, conversation.User2Nick);

                if (conv != null)
                {
                    conv.User1Nick = conversation.User1Nick;
                    conv.User2Nick = conversation.User2Nick;
                    conv.Messages = conversation.Messages;
                    conv.Id = conversation.Id;
                    conv.TimeCreated = conversation.TimeCreated;
                }
            }

            _context.SaveChanges();
        }

        public Conversation GetConversation(string login1, string login2)
        {
            return _context.Conversations.FirstOrDefault(c =>
                c.User1Nick == login1 | c.User1Nick == login2
                && c.User2Nick == login1 | c.User2Nick == login2);
        }

        public List<Conversation> GetConversations(string login)
        {
            return _context.Conversations
                .Where(c => c.User1Nick == login
                            || c.User2Nick == login).ToList();
        }
    }
}