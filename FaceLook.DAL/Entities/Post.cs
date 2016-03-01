#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

#endregion

namespace FaceLook.DAL.Entities
{
    public class Post
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int UserPostedId { get; set; }

        [ForeignKey("UserPostedId")]
        public virtual User UserPosted { get; set; }

        [DataType(DataType.MultilineText, ErrorMessage = "Please, write some information!")]
        public string Text { get; set; }

        [MaxLength(30)]
        public string Status { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Position { get; set; }

        [Display(Name = "Posted on")]
        public DateTime TimePosted { get; set; }

        [Required(ErrorMessage = "Please, enter a tag.")]
        public string Tag { get; set; }

        public virtual List<User> UsersLikedId { get; set; }
        public virtual List<PostAddition> PostAdditions { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}