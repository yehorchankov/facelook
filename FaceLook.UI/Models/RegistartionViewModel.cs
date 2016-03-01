#region

using System.ComponentModel.DataAnnotations;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.UI.Models
{
    public class RegistartionViewModel
    {
        public User User { get; set; }

        [Required(ErrorMessage = "Please, enter your login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Please, enter your password.")]
        public string Password { get; set; }
    }
}