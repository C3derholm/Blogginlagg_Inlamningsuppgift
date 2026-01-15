using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogginlägg_Inlämningsuppgift.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly BlogDbContext _context;

        public CommentService(BlogDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(CreateCommentDTO dto)
        {
            {
                var userExists = await _context.Users.AnyAsync(u => u.UserID == dto.UserID);
                if (!userExists)
                    throw new InvalidOperationException("User does not exist.");

                var post = await _context.Posts
                    .Select(p => new { p.PostID, p.UserID })
                    .FirstOrDefaultAsync(p => p.PostID == dto.PostID);

                if (post == null)
                    throw new InvalidOperationException("Post does not exist.");

                if (post.UserID == dto.UserID)
                    throw new InvalidOperationException("You cannot comment on your own post.");

                var comment = new Comment
                {
                    CommentText = dto.ContentText,
                    PostID = dto.PostID,
                    UserID = dto.UserID,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return comment.CommentID;
            }
        }

        public async Task<bool> DeleteAsync(int commentId, int userId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.CommentID == commentId);
            if (comment == null) return false;

            if (comment.UserID != userId)
            {
                throw new InvalidOperationException("You are not the owner of this comment.");
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<CommentDTO>> GetByPostIdAsync(int postId)
        {
            return await _context.Comments
            .Where(c => c.PostID == postId)
            .OrderBy(c => c.CreatedAt)
            .Select(c => new CommentDTO
            {
                CommentID = c.CommentID,
                ContentText = c.CommentText,
                CreatedAt = c.CreatedAt,
                PostID = c.PostID,
                UserName = c.User.Username
            })
            .ToListAsync();
        }
    }
}
