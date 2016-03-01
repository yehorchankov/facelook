#region

using System.Collections.Generic;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.BL.Abstract
{
    public interface IConversationManager
    {
        void SendMessage(Message message, string receiverLogin);
        Conversation CreateConversation(string login1, string login2);
        List<Message> GetMessagesForConversation(string login1, string login2);
        List<Conversation> GetConversations(string login);
    }
}