#region

using System.Collections.Generic;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.UI.Models
{
    public class MessageViewModel
    {
        public List<Message> Messages { get; set; }
        public Message Message { get; set; }
        public string ReceiverLogin { get; set; }
    }
}