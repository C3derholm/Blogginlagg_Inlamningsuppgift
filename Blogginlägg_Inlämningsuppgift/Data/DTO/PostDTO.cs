namespace Blogginlägg_Inlämningsuppgift.Data.DTO
{
    public class PostDTO
    {
        public int PostID { get; set; }
        public string Title { get; set; } = null!;
        public string ContentText { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } = null!;
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } = null!;
        
    }
}
