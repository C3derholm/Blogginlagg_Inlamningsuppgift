using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Data.Entities;


namespace Blogginlägg_Inlämningsuppgift.Core.Mapping
{
    public static class UserMappingExtensions
    {
       public static UserDTO ToDTO(this User user)
        {
            return new UserDTO
            {
                UserID = user.UserID,
                Username = user.Username,
                
            };
        }
    }
}
