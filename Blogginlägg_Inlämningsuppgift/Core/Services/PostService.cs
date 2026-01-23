using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Core.Mapping;
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

        public async Task<int> CreateAsync(CreatePostDTO dto, int userId)
        {
            bool userExists = await _context.Users.AnyAsync(u => u.UserID == userId);
            if (!userExists) throw new InvalidOperationException("User does not exist");

            bool categoryExists = await _context.Categories.AnyAsync(c => c.CategoryID == dto.CategoryID);
            if (!categoryExists) throw new InvalidOperationException("Category does not exist");


            var post = new Post
            {
                Title = dto.Title,
                ContentText = dto.ContentText,
                CreatedAt = DateTime.UtcNow,                
                CategoryID = dto.CategoryID,
                UserID= userId
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return post.PostID;

        }

        public async Task<bool> DeleteAsync(int postId, int userId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostID == postId);
            if (post == null) 
                return false;

            if (post.UserID != userId)
            {
                throw new InvalidOperationException("You are not the owner of this Blogpost");
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return true;
        }
     
        public async Task<List<PostDTO>> GetAllAsync()
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => p.ToDTO())                                                     
                .ToListAsync();


        }

        public async Task<PostDTO?> GetByIdAsync(int postId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Where(p => p.PostID == postId)
                .Select(p => p.ToDTO())
                .FirstOrDefaultAsync();

        }

        public async Task<List<PostDTO>> SearchByCategoryAsync(string categoryName)
        {
            var name= categoryName.Trim();

            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName.ToLower().Contains(name.ToLower()))
                .Select(p => p.ToDTO())
                .ToListAsync();

        }

        public async Task<List<PostDTO>> SearchByTitleAsync(string query)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Where(p => p.Title.Contains(query))
                .Select(p => p.ToDTO())
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(int postId, int userID, UpdatePostDTO dto)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostID == postId);
            if (post == null) return false;

            if (post.UserID != userID)
            {
               throw new InvalidOperationException("You are not the owner of this Blogpost");

            }

            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryID == dto.CategoryID);
            if (!categoryExists) throw new InvalidOperationException("Category does not exist");

            post.Title = dto.Title; 
            post.ContentText = dto.ContentText;
            post.CategoryID = dto.CategoryID;
            

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
