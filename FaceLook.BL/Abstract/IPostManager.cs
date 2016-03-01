#region

using FaceLook.DAL.Entities;

#endregion

namespace FaceLook.BL.Abstract
{
    public interface IPostManager
    {
        void Publicate(Post post, string login);
        Post RemovePost(int id, string login);
        int LikePost(int postId, User userLiked, string login);
        void CommentPost(Comment comment, int postId, string login);
    }
}