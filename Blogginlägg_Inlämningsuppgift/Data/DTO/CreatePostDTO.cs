namespace Blogginlägg_Inlämningsuppgift.Data.DTO
{
    public class CreatePostDTO
    {
        public string Title { get; set; } = null!;

        public string ContentText { get; set; } = null!;

        public int UserID { get; set; }

        public int CategoryID { get; set; }
    }
}
