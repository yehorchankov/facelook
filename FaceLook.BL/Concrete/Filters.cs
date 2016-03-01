#region

using System;
using System.Collections.Generic;
using System.Linq;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.BL.Concrete
{
    public class Filters
    {
        private readonly IUserRepository _repository;

        public Filters(IUserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Post> FindPostsByTag(string tag)
        {
            List<Post> posts = new List<Post>();

            foreach (User user in _repository.Users
                .Where(user => user.Posts != null))
            {
                posts.AddRange(user.Posts.Where(p =>
                    string.Equals(p.Tag, tag, StringComparison.InvariantCultureIgnoreCase)));
            }

            return posts;
        }

        public IEnumerable<User> FindUsersByName(string name)
        {
            switch (name)
            {
                case "*":
                    return _repository.Users;
                default:
                    return _repository.Users.Select(u => u)
                        .Where(user => user.FirstName.ToLower().Contains(name.ToLower()) ||
                                       user.LastName.ToLower().Contains(name.ToLower()));
            }
        }
    }
}