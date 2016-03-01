#region

using System.Collections.Generic;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.UI.Models
{
    public class SearchResult
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public User CurrentUser { get; set; }
    }
}