using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FaceLook.DAL.Entities
{
    public class Post
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public User UserPosted { get; set; }

        [DataType(DataType.MultilineText, ErrorMessage = "Please, write some information!")]
        public string Text { get; set; }

        [MaxLength(30)]
        public string Status { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string Position { get; set; }

        [Display(Name = "Posted on"), ]
        public DateTime TimePosted { get; set; }

        [Required(ErrorMessage = "Please, enter a tag.")]
        public string Tag { get; set; }

        public List<User> UsersLikedId { get; set; }

        public List<PostAddition> PostAdditions { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
