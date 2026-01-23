namespace Blogginlägg_Inlämningsuppgift.Data.DTO
{
    public class CreateCommentDTO
    {
        public string ContentText { get; set; } = null!;
        public int PostID { get; set; }
        
    }
}
