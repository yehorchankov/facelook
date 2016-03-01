#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FaceLook.BL.Abstract;
using FaceLook.BL.Concrete;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Entities;
using FaceLook.UI.Models;

#endregion

namespace FaceLook.UI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IPostManager _postManager;
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _postManager = new PostManager(_userRepository);
        }

        public ActionResult Page(string login)
        {
            User currentUser = _userRepository.GetUserByNickname(User.Identity.Name);

            UserViewModel userVm = new UserViewModel
            {
                CurrentUser = currentUser,
                NewPost = new Post(),
                Comment = new Comment()
            };

            if (string.IsNullOrEmpty(login))
            {
                userVm.User = currentUser;
                return View(userVm);
            }

            User user = _userRepository.GetUserByNickname(login);

            if (user == null)
                return RedirectToAction("PageNotFoundResult", "Error");

            userVm.User = user;

            return View(userVm);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Login");
        }

        public ActionResult Edit()
        {
            User currentUser = _userRepository.GetUserByNickname(User.Identity.Name);

            return View(currentUser);
        }

        [HttpPost]
        public ActionResult Edit(User user, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    user.MimeType = image.ContentType;
                    user.Photo = new byte[image.ContentLength];
                    image.InputStream.Read(user.Photo, 0, image.ContentLength);
                }
                _userRepository.SaveUser(user);

                TempData["message"] = "Profile updated successfully";
            }
            return RedirectToAction("Page", new {login = user.NickName});
        }

        public FileContentResult GetImage(string login)
        {
            User user = _userRepository.GetUserByNickname(login);

            return user != null
                ? File(user.Photo, user.MimeType)
                : null;
        }

        [HttpPost]
        public ActionResult Publish(string login, IEnumerable<HttpPostedFileBase> images, Post post)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Page", new {login});
            }

            User currentUser = _userRepository.GetUserByNickname(User.Identity.Name);

            post.UserPosted = currentUser;
            post.UsersLikedId = new List<User>();
            post.PostAdditions = new List<PostAddition>();
            post.Comments = new List<Comment>();
            post.TimePosted = DateTime.Now;


            if (images != null)
            {
                foreach (var file in images.Where(f => f != null))
                {
                    PostAddition postAddition = new PostAddition
                    {
                        MimeType = file.ContentType,
                        Data = new byte[file.ContentLength]
                    };
                    file.InputStream.Read(postAddition.Data, 0, file.ContentLength);

                    post.PostAdditions.Add(postAddition);
                }
            }

            _postManager.Publicate(post, login);

            return RedirectToAction("Page", new {login});
        }

        [HttpPost]
        public ActionResult Comment(Comment comment, string login, int postId)
        {
            User currentUser = _userRepository.GetUserByNickname(User.Identity.Name);

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Page", new {login});
            }

            comment.TimeCommented = DateTime.Now;
            comment.CommentOwner = currentUser;

            _postManager.CommentPost(comment, postId, login);

            return RedirectToAction("Page", new {login});
        }

        [HttpPost]
        public ActionResult Like(string login, int postId)
        {
            User currentUser = _userRepository.GetUserByNickname(User.Identity.Name);

            _postManager.LikePost(postId, currentUser, login);

            return RedirectToAction("Page", new {login});
        }

        [HttpPost]
        public ActionResult RemovePost(string login, int postId)
        {
            _postManager.RemovePost(postId, login);

            return RedirectToAction("Page", new {login});
        }
    }
}