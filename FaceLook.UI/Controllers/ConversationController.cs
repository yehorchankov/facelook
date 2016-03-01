#region

using System;
using System.Web.Mvc;
using FaceLook.BL.Abstract;
using FaceLook.BL.Concrete;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Concrete;
using FaceLook.DAL.Entities;
using FaceLook.UI.Models;

#endregion

namespace FaceLook.UI.Controllers
{
    [Authorize]
    public class ConversationController : Controller
    {
        private readonly IConversationManager _conversationManager;
        private readonly IUserRepository _userRepository;

        public ConversationController()
        {
            EfDbContext dbContext = new EfDbContext();

            _userRepository = new UserRepository(dbContext);
            _conversationManager = new ConversationManager(_userRepository);
        }

        // GET: Messages
        public ActionResult Index(string login)
        {
            if (_userRepository.GetUserByNickname(login) == null)
            {
                return RedirectToAction("PageNotFoundResult", "Error");
            }

            MessageViewModel messageVm = new MessageViewModel
            {
                Message = new Message
                {
                    Sender = _userRepository.GetUserByNickname(User.Identity.Name),
                    Text = ""
                },
                Messages = _conversationManager
                    .GetMessagesForConversation(User.Identity.Name, login),
                ReceiverLogin = login
            };

            messageVm.Messages.Reverse();

            return View(messageVm);
        }

        [HttpPost]
        public ActionResult Index(MessageViewModel messageVm)
        {
            if (!ModelState.IsValid)
            {
                return View(messageVm);
            }

            messageVm.Message.TimeSent = DateTime.Now;
            messageVm.Message.Sender =
                _userRepository.GetUserByNickname(User.Identity.Name);

            _conversationManager
                .SendMessage(messageVm.Message, messageVm.ReceiverLogin);

            return RedirectToAction("Index", new {login = messageVm.ReceiverLogin});
        }

        public ActionResult List()
        {
            return View(_conversationManager
                .GetConversations(User.Identity.Name));
        }
    }
}