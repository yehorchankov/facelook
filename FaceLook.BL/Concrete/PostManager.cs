#region

using System.Collections.Generic;
using System.Linq;
using FaceLook.BL.Abstract;
using FaceLook.DAL.Abstract;
using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.BL.Concrete
{
    public class PostManager : IPostManager
    {
        private readonly IUserRepository _repository;

        /// <summary>
        /// </summary>
        /// <param name="repository">User repository</param>
        public PostManager(IUserRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        ///     Publicate a <code>post</code> on the page of user
        /// </summary>
        /// <param name="post">Post to publish</param>
        /// <param name="login">User who owns the wall</param>
        public void Publicate(Post post, string login)
        {
            User user = _repository.GetUserByNickname(login);

            if (user == null)
                return;

            if (user.Posts == null)
                user.Posts = new List<Post>();

            user.Posts.Add(post);

            SaveChanges(user);
        }

        /// <summary>
        ///     Breaks all dependencies and remove post by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="login">User who owns the wall</param>
        /// <returns>Removed post</returns>
        public Post RemovePost(int id, string login)
        {
            User user = _repository.GetUserByNickname(login);

            if (user == null)
                return null;

            if (user.Posts == null)
                user.Posts = new List<Post>();

            Post postToRemove = user.Posts
                .FirstOrDefault(p => p.Id == id);

            if (postToRemove == null)
                return null;
            postToRemove.Comments.Clear();
            postToRemove.UsersLikedId.Clear();
            postToRemove.PostAdditions.Clear();
            user.Posts.Remove(postToRemove);

            SaveChanges(user);

            return postToRemove;
        }

        /// <summary>
        ///     Provides logis to like or dislike post by user
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userLiked">User who liked post</param>
        /// <param name="login">User who owns the wall</param>
        /// <returns>Likes count</returns>
        public int LikePost(int postId, User userLiked, string login)
        {
            User user = _repository.GetUserByNickname(login);

            if (user == null) return 0;

            if (user.Posts == null)
                user.Posts = new List<Post>();

            Post postToLike = user.Posts
                .FirstOrDefault(p => p.Id == postId);

            if (postToLike == null) return 0;

            if (postToLike.UsersLikedId.Contains(userLiked))
            {
                postToLike.UsersLikedId.Remove(userLiked);
            }
            else
            {
                postToLike.UsersLikedId.Add(userLiked);
            }

            SaveChanges(user);

            return postToLike.UsersLikedId.Count;
        }

        /// <summary>
        ///     Add comment to post
        /// </summary>
        /// <param name="comment">Comment</param>
        /// <param name="postId">Post id</param>
        /// <param name="login">User who owns the wall</param>
        public void CommentPost(Comment comment, int postId, string login)
        {
            User user = _repository.GetUserByNickname(login);

            if (user == null) return;

            if (user.Posts == null)
                user.Posts = new List<Post>();

            Post post = user.Posts
                .FirstOrDefault(p => p.Id == postId);

            if (post != null) post.Comments.Add(comment);

            SaveChanges(user);
        }

        private void SaveChanges(User user)
        {
            _repository.SaveUser(user);
        }
    }
}