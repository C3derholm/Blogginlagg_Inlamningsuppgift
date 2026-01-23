using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Data.Entities;


namespace Blogginlägg_Inlämningsuppgift.Core.Mapping
{
    public static class PostMappingExtensions
    {
        public static PostDTO ToDTO(this Post post)
        {
            return new PostDTO
            {
                PostID = post.PostID,
                Title = post.Title,
                ContentText = post.ContentText,
                CreatedAt = post.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                UserID = post.UserID,
                CategoryID = post.CategoryID,
                CategoryName = post.Category?.CategoryName,
                UserName = post.User?.Username
            };
        }                          
    }
}
