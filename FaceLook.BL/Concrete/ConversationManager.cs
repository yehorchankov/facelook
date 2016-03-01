#region

using System;
using System.Collections.Generic;
using FaceLook.BL.Abstract;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Concrete;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.BL.Concrete
{
    public class ConversationManager : IConversationManager
    {
        private readonly IUserRepository _repository;

        public ConversationManager(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public ConversationManager(EfDbContext dbContext)
        {
            _repository = new UserRepository(dbContext);
        }

        /// <summary>
        ///     Sends message to both participants and save changes
        /// </summary>
        /// <param name="message"></param>
        /// <param name="receiverLogin"></param>
        public void SendMessage(Message message, string receiverLogin)
        {
            Conversation conversation = _repository
                .GetConversation(message.Sender.NickName, receiverLogin) ??
                                        CreateConversation(message.Sender.NickName, receiverLogin);

            conversation.Messages.Add(message);

            _repository.SaveConversation(conversation);
        }

        /// <summary>
        ///     Creates conversations to both users
        /// </summary>
        /// <param name="login1"></param>
        /// <param name="login2"></param>
        /// <returns>New conversation</returns>
        public Conversation CreateConversation(string login1, string login2)
        {
            Conversation conversation = new Conversation
            {
                Id = 0,
                Messages = new List<Message>(),
                TimeCreated = DateTime.Now,
                User1Nick = login1,
                User2Nick = login2
            };

            _repository.SaveConversation(conversation);

            return conversation;
        }

        /// <summary>
        ///     Contains all messages from conversation
        /// </summary>
        /// <param name="login1"></param>
        /// <param name="login2"></param>
        /// <returns>Messages for conversation if exists or null</returns>
        public List<Message> GetMessagesForConversation(string login1, string login2)
        {
            Conversation conversation =
                _repository.GetConversation(login1, login2) ?? CreateConversation(login1, login2);

            return conversation.Messages;
        }

        /// <summary>
        ///     Returns all conversations for current <code>login</code>
        /// </summary>
        /// <param name="login">The user login to find aonversations</param>
        /// <returns></returns>
        public List<Conversation> GetConversations(string login)
        {
            return _repository.GetConversations(login);
        }
    }
}