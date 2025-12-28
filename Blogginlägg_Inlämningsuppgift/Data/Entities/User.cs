using System.ComponentModel.DataAnnotations;

namespace Blogginlägg_Inlämningsuppgift.Data.Entities
{
    public class User
    {
        [Key]

        public int UserID { get; set; }

        [StringLength (50)]
        public string Username { get; set; } = null!;

        [StringLength (100)]
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;


        public ICollection<Post> Posts { get; set; } = new List<Post>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();


    }
}
