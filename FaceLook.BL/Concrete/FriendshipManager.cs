#region

using System.Collections.Generic;
using System.Linq;
using FaceLook.BL.Abstract;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.BL.Concrete
{
    public class FriendshipManager : IFriendshipManager
    {
        private readonly IUserRepository _repository;

        public FriendshipManager(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///     Confirm that <code>user</code> who proposed friendship is actually a friend
        /// </summary>
        /// <param name="user">User is a new friend</param>
        /// <param name="login">Login of current user</param>
        public void ConfirmFriendship(User user, string login)
        {
            User currentUser = _repository.GetUserByNickname(login);

            if (currentUser == null) return;

            if (currentUser.FriendsToConfirm == null)
                currentUser.FriendsToConfirm = new List<FriendshipRequest>();

            if (currentUser.FriendsToConfirm.Any(f => f.Login == user.NickName))
            {
                currentUser.FriendsToConfirm.RemoveAll(f => f.Login == user.NickName);

                currentUser.Friends.Add(user);
                user.Friends.Add(currentUser);

                SaveChanges(user, currentUser);
            }
        }

        /// <summary>
        ///     Remove <code>user</code> from friendship proposals
        /// </summary>
        /// <param name="user">User who proposed friendship</param>
        /// <param name="login">Login of current user</param>
        public void DenyFriendship(User user, string login)
        {
            User currentUser = _repository.GetUserByNickname(login);

            if (currentUser == null) return;

            if (currentUser.FriendsToConfirm == null)
                currentUser.FriendsToConfirm = new List<FriendshipRequest>();

            if (currentUser.FriendsToConfirm.Any(f => f.Login == user.NickName))
            {
                currentUser.FriendsToConfirm.RemoveAll(f => f.Login == user.NickName);

                SaveChanges(user, currentUser);
            }
        }

        /// <summary>
        ///     Sends a friendship proposal to <code>user</code>
        /// </summary>
        /// <param name="user">User who will receive friendship proposal</param>
        /// <param name="login">Login of current user</param>
        public void AddToFriends(User user, string login)
        {
            User currentUser = _repository.GetUserByNickname(login);

            if (currentUser == null) return;

            if (user.FriendsToConfirm == null)
                user.FriendsToConfirm = new List<FriendshipRequest>();

            user.FriendsToConfirm.Add(new FriendshipRequest {Login = currentUser.NickName});

            SaveChanges(user, currentUser);
        }

        /// <summary>
        ///     Removes <code>user</code> from friends
        /// </summary>
        /// <param name="user">User to remove from frinds</param>
        /// <param name="login">Login of current user</param>
        public void RemoveFromFriends(User user, string login)
        {
            User currentUser = _repository.GetUserByNickname(login);

            if (currentUser == null) return;

            if (currentUser.Friends == null)
                currentUser.Friends = new List<User>();
            if (user.Friends == null)
                user.Friends = new List<User>();

            if (currentUser.Friends.Contains(user) && user.Friends.Contains(currentUser))
            {
                currentUser.Friends.Remove(user);
                user.Friends.Remove(currentUser);

                SaveChanges(user, currentUser);
            }
        }

        private void SaveChanges(User userSubject, User userObject)
        {
            _repository.SaveUser(userSubject);

            //if (userObject.Friends.Contains(userSubject))
            //    userObject.Friends.Remove(userSubject);

            _repository.SaveUser(userObject);
        }
    }
}