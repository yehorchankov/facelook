#region

using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.BL.Abstract
{
    public interface IFriendshipManager
    {
        void ConfirmFriendship(User user, string login);
        void DenyFriendship(User user, string login);
        void AddToFriends(User user, string login);
        void RemoveFromFriends(User user, string login);
    }
}