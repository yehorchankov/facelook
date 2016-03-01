#region

using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.UI.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public User CurrentUser { get; set; }
        public Post NewPost { get; set; }
        public Comment Comment { get; set; }

        public bool IsUserCurrent
        {
            get
            {
                if (User != null && CurrentUser != null)
                    return User.NickName == CurrentUser.NickName;
                return false;
            }
        }
    }
}