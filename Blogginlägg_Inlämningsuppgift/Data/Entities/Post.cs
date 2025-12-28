using System.ComponentModel.DataAnnotations;

namespace Blogginlägg_Inlämningsuppgift.Data.Entities
{
    public class Post
    {
        [Key]
        public int PostID { get; set; }

        [StringLength(100)]
        public string Title { get; set; } = null!;

        [StringLength(100)]
        public string ContentText { get; set; } = null!;

        public DateTime CreatedAt { get; set; }


        public int UserID { get; set; }

        public User User { get; set; } = null!;

        public int CategoryID { get; set; } 

        public Category Category { get; set; } 

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
