using Blogginlägg_Inlämningsuppgift.Data.DTO;

namespace Blogginlägg_Inlämningsuppgift.Core.Interfaces
{
    public interface ICommentService
    {
       
      Task<int> CreateAsync(CreateCommentDTO dTO, int userId);

      Task<List<CommentDTO>> GetByPostIdAsync(int postId);

      Task<bool> DeleteAsync(int commentId, int userId);
      Task GetAllAsync();
    }
}
