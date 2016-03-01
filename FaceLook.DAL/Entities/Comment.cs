#region

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

#endregion

namespace FaceLook.DAL.Entities
{
    public class Comment
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int CommentOwnerId { get; set; }

        [Required(ErrorMessage = "Please, enter a comment")]
        public string Text { get; set; }

        [ForeignKey("CommentOwnerId")]
        public virtual User CommentOwner { get; set; }

        public DateTime TimeCommented { get; set; }
    }
}