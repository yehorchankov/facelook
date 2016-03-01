#region

using System;
using System.Web;
using System.Web.Mvc;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Entities;
using FaceLook.UI.Infrastructure.Abstract;
using FaceLook.UI.Models;

#endregion

namespace FaceLook.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAuthProvider _authProvider;
        private readonly IUserRepository _userRepository;

        public LoginController(IAuthProvider authProvider, IUserRepository userRepository)
        {
            _authProvider = authProvider;
            _userRepository = userRepository;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Index(LoginViewModel loginVm)
        {
            User user = _userRepository.GetUserByNickname(loginVm.Login);

            if (user == null)
            {
                TempData["errorMessage"] = "Account was not found";
                return View(new LoginViewModel());
            }

            if (!user.AccountConfirmed)
            {
                TempData["errorMessage"] = "Account was not confirmed";
                return View(new LoginViewModel());
            }

            bool result = _authProvider.Authenticate(loginVm.Login, loginVm.Password);
            if (!result)
            {
                //ModelState.AddModelError("Error", "Login or password was incorrect");
                TempData["errorMessage"] = "Login or password was incorrect";
                return View(new LoginViewModel());
            }

            return RedirectToAction("Page", "Account", new {login = loginVm.Login});
        }

        public ActionResult Registrate()
        {
            RegistartionViewModel registrationVm = new RegistartionViewModel
            {
                User = new User()
            };

            return View(registrationVm);
        }

        [HttpPost]
        public ActionResult Registrate(RegistartionViewModel registartionVm, HttpPostedFileBase image = null)
        {
            if (_authProvider.IsLoginUsed(registartionVm.Login))
            {
                TempData["errorMessage"] = "User with the same login already exists. Please, choose cooler one";

                return View(registartionVm);
            }

            registartionVm.User.NickName = registartionVm.Login;

            if (!ModelState.IsValid)
            {
                return View(registartionVm);
            }

            TempData["message"] =
                "Account was successfully created. Please check your email inbox for confiramtion mail";

            if (image != null)
            {
                registartionVm.User.MimeType = image.ContentType;
                registartionVm.User.Photo = new byte[image.ContentLength];
                image.InputStream.Read(registartionVm.User.Photo, 0, image.ContentLength);
            }

            _userRepository.SaveUser(registartionVm.User);
            _authProvider.Registrate(registartionVm, Request.Url);

            return RedirectToAction("Index");
        }

        public ActionResult Confirm(string login, Guid key)
        {
            if (_authProvider.ConfirmAccount(login, key))
            {
                TempData["message"] = "Account was successfully confirmed";
            }
            else
            {
                TempData["errorMessage"] = "Account wasn't confirmed";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public string LoginUsed(string login)
        {
            if (_authProvider.IsLoginUsed(login))
            {
                string message = "User with the same login already exists. Please, choose cooler login";
                ModelState.AddModelError("loginUsed", message);
                TempData["errorMessage"] = message;

                return message;
            }
            return "OK";
        }
    }
}