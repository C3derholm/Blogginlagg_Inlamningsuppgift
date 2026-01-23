using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Data.Entities;

namespace Blogginlägg_Inlämningsuppgift.Core.Mapping
{
    public static class CommentMappingExtensions
    {
        public static CommentDTO ToDTO(this Comment comment)
        {
            return new CommentDTO
            {
                CommentID = comment.CommentID,
                ContentText = comment.CommentText,
                CreatedAt = comment.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                PostID = comment.PostID,
                UserName = comment.User?.Username
            };                  
        }                          
    }
}
