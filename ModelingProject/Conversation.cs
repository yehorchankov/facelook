using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FaceLook.DAL.Entities
{
    public class Conversation
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "User 1")]
        public User User1 { get; set; }

        [Display(Name = "User 2")]
        public User User2 { get; set; }

        public DateTime TimeCreated { get; set; }

        public List<Message> Messages { get; set; }
    }
}
