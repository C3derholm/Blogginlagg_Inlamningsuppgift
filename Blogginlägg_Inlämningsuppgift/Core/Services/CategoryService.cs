using Blogginlägg_Inlämningsuppgift.Core.Interfaces;
using Blogginlägg_Inlämningsuppgift.Data;
using Blogginlägg_Inlämningsuppgift.Data.DTO;
using Blogginlägg_Inlämningsuppgift.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blogginlägg_Inlämningsuppgift.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly BlogDbContext _context;

        public CategoryService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            return await _context.Categories
                .Select(c => new CategoryDTO
                {
                    CategoryID = c.CategoryID,
                    CategoryName = c.CategoryName
                })
                .ToListAsync();
        }

        


            

        
    }
}
