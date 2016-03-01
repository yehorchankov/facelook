using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FaceLook.DAL.Entities
{
    public class Message
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Message cannot be empty.")]
        public string Text { get; set; }

        public bool Seen { get; set; }
        
        public User Sender { get; set; }
        
        public DateTime TimeSent { get; set; }
    }
}
