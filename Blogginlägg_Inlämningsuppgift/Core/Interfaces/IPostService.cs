using Blogginlägg_Inlämningsuppgift.Data.DTO;

namespace Blogginlägg_Inlämningsuppgift.Core.Interfaces
{
    public interface IPostService
    {
        Task<int> CreateAsync(CreatePostDTO dto);
        Task<PostDTO?> GetByIdAsync(int postId);

        Task<List<PostDTO>> GetAllAsync();

        Task<bool> UpdateAsync(int postId,int userId, UpdatePostDTO dto);

        Task <bool>DeleteAsync(int postId,int userId);

        Task<List<PostDTO>> SearchByTitleAsync(string query);
        Task<List<PostDTO>> SearchByCategoryAsync(string categoryName);
    }
}
