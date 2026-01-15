using Blogginlägg_Inlämningsuppgift.Data.DTO;

namespace Blogginlägg_Inlämningsuppgift.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetAllAsync();

        


    }
}
