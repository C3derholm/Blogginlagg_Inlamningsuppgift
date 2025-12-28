namespace Blogginlägg_Inlämningsuppgift.Data.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public int Name { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
