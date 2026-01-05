namespace Blogginlägg_Inlämningsuppgift.Data.DTO
{
    public class UpdatePostDTO
    {
        public string Title { get; set; } = null!;

        public string ContentText { get; set; } = null!;

        public int CategoryID { get; set; }

        public int UserID { get; set; }
    }
}
