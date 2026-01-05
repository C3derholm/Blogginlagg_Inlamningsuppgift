using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blogginlägg_Inlämningsuppgift.Core.Services
{
    public class UserService : IUserService
    {
        public readonly BlogDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher= new PasswordHasher<User>();

        public UserService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task DeleteAsync(int userid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == userid);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var userPosts = await _context.Posts.Where(p => p.UserID == userid).ToListAsync();

            var commentsOnUserPosts = await _context.Comments
                .Where(c => userPosts.Select(p => p.PostID).Contains(c.PostID))
                .ToListAsync();

            var userComments = await _context.Comments
                .Where(c => c.UserID == userid)
                .ToListAsync();

            _context.Comments.RemoveRange(commentsOnUserPosts);
            _context.Comments.RemoveRange(userComments);

            _context.Posts.RemoveRange(userPosts);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<UserDTO?> GetByIdAsync(int userid)
        {
            return await _context.Users
                .Where(u => u.UserID == userid)
                .Select(u => new UserDTO
                {
                    UserID = u.UserID,
                    Username = u.Username,
                    Email = u.Email
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int?> LoginAsync(LoginDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result != PasswordVerificationResult.Success)
            {
                return null;
            }

            return user.UserID;

        }

        public async Task<int> RegisterAsync(RegisterUserDTO dto)
        {
            var exists = await _context.Users.AnyAsync(u => u.Username == dto.Username || u.Email == dto.Email);

            if (exists)
            {
                throw new Exception("User with same username or email already exists");
            }

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash= _passwordHasher.HashPassword(new User(), dto.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user.UserID;
        }

        public Task UpdateAsync(int userid, UpdateUserDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
