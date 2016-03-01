using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FaceLook.DAL.Entities
{
    public class User
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter your first name."), 
            Display(Name = "First name"), MaxLength(50)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please, enter your last name."), 
            Display(Name = "Last name"), MaxLength(50)]
        public string LastName { get; set; }

        [Display(Name = "Middle name"), MinLength(1), MaxLength(50)]
        public string MiddleName { get; set; }

        [Display(Name = "Birth date"), DataType(DataType.DateTime), 
            Required(ErrorMessage = "Please, enter your birth date.")]
        public DateTime BirthDate { get; set; }

        [Display(AutoGenerateField = false), HiddenInput(DisplayValue = false)]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Login")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "Please, enter your email address."), DataType(DataType.EmailAddress, 
            ErrorMessage = "Please, be sure to enter correct email address.")]
        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        [Display(AutoGenerateField = false)]
        public string AccountStatus { get; set; }

        [Display(AutoGenerateField = false)]
        public bool AccountConfirmed { get; set; }

        [MaxLength(1)]
        public string Gender { get; set; }

        [DataType(DataType.MultilineText)]
        public string Information { get; set; }

        public byte[] Photo { get; set; }
        public string MimeType { get; set; }

        public List<Post> Posts { get; set; }
        public List<User> Friends { get; set; }
        public List<User> FriendsToConfirm { get; set; }
        public List<Conversation> Conversations { get; set; }
    }
}
