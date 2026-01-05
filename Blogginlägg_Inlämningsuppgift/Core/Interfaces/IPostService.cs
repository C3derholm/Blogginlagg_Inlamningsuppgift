using Blogginlägg_Inlämningsuppgift.Data.DTO;

namespace Blogginlägg_Inlämningsuppgift.Core.Interfaces
{
    public interface IPostService
    {
        Task<int> CreateAsync(PostDTO dto);
        Task<PostDTO?> GetByIdAsync(int postId);

        Task<List<PostDTO>> GetAllAsync();

        Task UpdateAsync(int postId, PostDTO dto);

        Task DeleteAsync(int postId);

        Task<List<PostDTO>> SearchByTitleAsync(string query);
        Task<List<PostDTO>> SearchByCategoryAsync(string categoryName);
    }
}
