using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FaceLook.DAL.Entities
{
    public class Comment
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter a comment")]
        public string Text { get; set; }

        public User CommentOwner { get; set; }

        public DateTime TimeCommented { get; set; }
    }
}
