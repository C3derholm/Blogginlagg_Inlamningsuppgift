using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Data.Entities;

namespace Blogginlägg_Inlämningsuppgift.Core.Mapping
{
    public static class CategoryMappingExtensions
    {
        public static CategoryDTO ToDTO(this Category category)
        {
            return new CategoryDTO
            {
                CategoryID = category.CategoryID,
                CategoryName = category.CategoryName
            };
        }
    }
}
