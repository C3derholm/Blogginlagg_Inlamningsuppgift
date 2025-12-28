using System.ComponentModel.DataAnnotations;

namespace Blogginlägg_Inlämningsuppgift.Data.Entities
{
    public class Category
    {

        [Key]
        public int CategoryID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; } = null!;

        public ICollection<Post> Posts { get; set; } = new List<Post>();


    }
}
