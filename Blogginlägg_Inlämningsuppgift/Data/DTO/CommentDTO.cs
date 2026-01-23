namespace Blogginlägg_Inlämningsuppgift.Data.DTO
{
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public string ContentText { get; set; } = null!;
        public string CreatedAt { get; set; } = null!;
        public int PostID { get; set; }
        public string UserName { get; set; } = null!;
    }
}
