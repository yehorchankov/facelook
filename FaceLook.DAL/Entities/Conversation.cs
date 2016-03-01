#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

#endregion

namespace FaceLook.DAL.Entities
{
    public class Conversation
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "User 1")]
        public string User1Nick { get; set; }

        [Display(Name = "User 2")]
        public string User2Nick { get; set; }

        public DateTime TimeCreated { get; set; }
        public virtual List<Message> Messages { get; set; }
    }
}