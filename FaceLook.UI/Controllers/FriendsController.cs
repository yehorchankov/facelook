#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FaceLook.BL.Abstract;
using FaceLook.BL.Concrete;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Entities;
using FaceLook.UI.Models;

#endregion

namespace FaceLook.UI.Controllers
{
    [Authorize]
    public class FriendsController : Controller
    {
        private readonly IFriendshipManager _friendshipManager;
        private readonly IUserRepository _userRepository;

        public FriendsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _friendshipManager = new FriendshipManager(_userRepository);
        }

        // GET: Friends
        public ActionResult Index(string login)
        {
            //Check if request is for current user's friends, if true show friends for current user
            User user;

            if (string.IsNullOrEmpty(login) || login == User.Identity.Name)
            {
                user = _userRepository.GetUserByNickname(User.Identity.Name);

                return View("MyFriends", user.Friends);
            }

            user = _userRepository.GetUserByNickname(login);

            if (user == null)
            {
                return RedirectToAction("PageNotFoundResult", "Error");
            }

            UserViewModel userVm = new UserViewModel
            {
                CurrentUser = _userRepository.GetUserByNickname(User.Identity.Name),
                User = user
            };

            return View(userVm);
        }

        public ActionResult FriendsToConfirm()
        {
            List<User> friendsToConfirm =
                _userRepository.GetUserByNickname(User.Identity.Name)
                    .FriendsToConfirm.Select(conf => _userRepository
                        .GetUserByNickname(conf.Login)).ToList();

            return View(friendsToConfirm);
        }

        [HttpPost]
        public ActionResult Confirm(string login)
        {
            User userToConfirm = _userRepository.GetUserByNickname(login);
            _friendshipManager.ConfirmFriendship(userToConfirm, User.Identity.Name);

            TempData["message"] =
                string.Format("You have confirmed friendship proposal from {0} {1}",
                    userToConfirm.FirstName, userToConfirm.LastName);

            return Index(User.Identity.Name);
        }

        [HttpPost]
        public ActionResult Deny(string login)
        {
            User user = _userRepository.GetUserByNickname(login);
            _friendshipManager.DenyFriendship(user, User.Identity.Name);

            TempData["message"] =
                string.Format("You have denied friendship proposal from {0} {1}",
                    user.FirstName, user.LastName);

            return Index(User.Identity.Name);
        }

        [HttpPost]
        public ActionResult Add(string login)
        {
            User user = _userRepository.GetUserByNickname(login);
            _friendshipManager.AddToFriends(user, User.Identity.Name);

            TempData["message"] =
                string.Format("You have sent friendship proposal to {0} {1}",
                    user.FirstName, user.LastName);

            return Index(User.Identity.Name);
        }

        [HttpPost]
        public ActionResult Remove(string login)
        {
            User user = _userRepository.GetUserByNickname(login);
            _friendshipManager.RemoveFromFriends(user, User.Identity.Name);

            TempData["message"] =
                string.Format("You have removed {0} {1} from your friends",
                    user.FirstName, user.LastName);

            return Index(User.Identity.Name);
        }
    }
}