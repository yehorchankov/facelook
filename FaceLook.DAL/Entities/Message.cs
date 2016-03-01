#region

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

#endregion

namespace FaceLook.DAL.Entities
{
    public class Message
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int SenderId { get; set; }

        [Required(ErrorMessage = "Message cannot be empty.")]
        public string Text { get; set; }

        public bool Seen { get; set; }

        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        public DateTime TimeSent { get; set; }
    }
}