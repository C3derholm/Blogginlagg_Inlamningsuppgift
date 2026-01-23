using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Core.Mapping;
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
        public async Task<int> CreateAsync(CreateCommentDTO dto, int userId)
        {
            {
                var userExists = await _context.Users.AnyAsync(u => u.UserID == userId);
                if (!userExists)
                    throw new InvalidOperationException("User does not exist.");

                var post = await _context.Posts
                    .Select(p => new { p.PostID, p.UserID })
                    .FirstOrDefaultAsync(p => p.PostID == dto.PostID);

                if (post == null)
                    throw new InvalidOperationException("Post does not exist.");

                if (post.UserID == userId)
                    throw new InvalidOperationException("You cannot comment on your own post.");

                var comment = new Comment
                {
                    CommentText = dto.ContentText,
                    PostID = dto.PostID,
                    UserID = userId,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();
                return comment.CommentID;
            }
        }

        public async Task<bool> DeleteAsync(int commentId, int userId)
        {
            var comment = await _context.Comments.Include(c=>c.Post).FirstOrDefaultAsync(c => c.CommentID == commentId);
            if (comment == null) return false;

            bool isCommentOwner = comment.UserID == userId;
            bool isPostOwner = comment.Post?.UserID == userId;
           
            if(!isCommentOwner && !isPostOwner)
            {
                throw new InvalidOperationException("You are not authorized to delete this comment");
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
            .Include(c => c.User)
            .Where(c => c.PostID == postId)
            .OrderBy(c => c.CreatedAt)
            .Select(c => c.ToDTO())
            .ToListAsync();
        }
    }
}
