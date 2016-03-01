#region

using System.Collections.Generic;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.DAL.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        int SaveUser(User user);
        User DeleteUser(int userId);
        User GetUserById(int Id);
        User GetUserByNickname(string nickname);
        void SaveConversation(Conversation conversation);
        Conversation GetConversation(string login1, string login2);
        List<Conversation> GetConversations(string login);
    }
}