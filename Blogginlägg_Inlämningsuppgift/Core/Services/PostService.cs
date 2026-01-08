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

        public async Task<int> CreateAsync(CreatePostDTO dto)
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
                .OrderByDescending(p => p.CreatedAt)
                .Select(p => new PostDTO
                {
                    PostID = p.PostID,
                    Title = p.Title,
                    ContentText = p.ContentText,
                    CreatedAt = p.CreatedAt,
                    UserID = p.UserID,
                    UserName = p.User.Username,
                    CategoryName = p.Category.CategoryName,
                    CategoryID = p.CategoryID

                    })
                .ToListAsync();


        }

        public async Task<PostDTO?> GetByIdAsync(int postId)
        {
            return await _context.Posts
                .Where(p => p.PostID == postId)
                .Select(p => new PostDTO
                {
                    PostID = p.PostID,
                    Title = p.Title,
                    ContentText = p.ContentText,
                    CreatedAt = p.CreatedAt,
                    UserID = p.UserID,
                    UserName = p.User.Username,
                    CategoryName = p.Category.CategoryName,
                    CategoryID = p.CategoryID
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<PostDTO>> SearchByCategoryAsync(int categoryId)
        {
            return await _context.Posts
                .Where(p => p.CategoryID == categoryId)
                .Select(p => new PostDTO
                {
                    PostID = p.PostID,
                    Title = p.Title,
                    ContentText = p.ContentText,
                    CreatedAt = p.CreatedAt,
                    UserID = p.UserID,
                    CategoryID = p.CategoryID,
                })
                .ToListAsync();

        }

        public async Task<List<PostDTO>> SearchByTitleAsync(string query)
        {
            return await _context.Posts
                .Where(p => p.Title.Contains(query))
                .Select(p => new PostDTO
                {
                    PostID= p.PostID,
                    Title= p.Title,
                    ContentText= p.ContentText,
                    CreatedAt= p.CreatedAt,
                    UserID= p.UserID,
                    CategoryID= p.CategoryID

                })
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
