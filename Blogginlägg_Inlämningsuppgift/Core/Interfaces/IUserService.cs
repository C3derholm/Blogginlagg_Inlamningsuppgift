using Blogginlägg_Inlämningsuppgift.Data.Entities;
using Blogginlägg_Inlämningsuppgift.Data.DTO;

namespace Bloginlägg_Inlämningsuppgift.Core.Interfacs
{
    public interface IUserService
    {
        Task<int> RegisterAsync(RegisterUserDTO dto);

        Task<int> LoginAsync(LoginDTO dto);

        Task UpdateAsync(int userid, UpdateUserDTO dto);

        Task DeleteAsync(int userid);

        Task<User?> GetByIdAsync(int userid);
    }
}
