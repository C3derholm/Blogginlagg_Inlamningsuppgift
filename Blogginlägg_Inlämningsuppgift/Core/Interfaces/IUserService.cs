using Blogginlägg_Inlämningsuppgift.Data.Entities;
using Blogginlägg_Inlämningsuppgift.Data.DTO;

namespace Blogginlägg_Inlämningsuppgift.Core.Interfaces
{
    public interface IUserService
    {
        Task<int> RegisterAsync(RegisterUserDTO dto);

        Task<int?> LoginAsync(LoginDTO dto);

        Task UpdateAsync(int userid, UpdateUserDTO dto);

        Task DeleteAsync(int userid);

        Task<UserDTO?> GetByIdAsync(int userid);
    }
}
