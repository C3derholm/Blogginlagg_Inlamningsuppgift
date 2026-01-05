using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogginlägg_Inlämningsuppgift.Core.Services
{
    public class PostService : IPostService
    {
        private readonly BlogDbContext _context;

        public PostService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(PostDTO dto)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.UserID == dto.UserID);
            if (!userExists) throw new InvalidOperationException("User does not exist");

            bool categoryExists = await _context.Categories.AnyAsync(c => c.CategoryID == dto.CategoryID);
            if (!categoryExists) throw new InvalidOperationException("Category does not exist");


            var post = new Post
            {
                Title = dto.Title,
                ContentText = dto.ContentText,
                CreatedAt = DateTime.UtcNow,
                UserID = dto.UserID,
                CategoryID = dto.CategoryID
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post.PostID;

        }

        public Task DeleteAsync(int postId)
        {
            throw new NotImplementedException();
        }
     
        public Task<List<PostDTO>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PostDTO?> GetByIdAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostDTO>> SearchByCategoryAsync(string categoryName)
        {
            throw new NotImplementedException();
        }

        public Task<List<PostDTO>> SearchByTitleAsync(string query)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int postId, PostDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
