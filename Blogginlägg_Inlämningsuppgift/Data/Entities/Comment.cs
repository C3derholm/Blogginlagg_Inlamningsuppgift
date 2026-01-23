using System.ComponentModel.DataAnnotations;

namespace Blogginlägg_Inlämningsuppgift.Data.Entities
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [StringLength(2000)]
        public string CommentText { get; set; } 

        public DateTime CreatedAt { get; set; }


        public int PostID { get; set; } 
        public Post? Post { get; set; } 
        public int UserID { get; set; }
        public User? User { get; set; } 



    }
}
