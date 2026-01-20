namespace Blogginlägg_Inlämningsuppgift.Core.Interfaces
{
    public interface ITokenService
    {               
       string CreateToken(int userId, string username);      
    }
}
