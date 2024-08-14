using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Entities.RoomEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface ICategoryService
    {
        Task<CategoryToReturnDto?> CreateCategoryAsync(CategoryDto categoryDto);
        Task<IReadOnlyList<CategoryToReturnDto>> GetAllCategories();
        Task<CategoryToReturnDto?> GetCategoryById(int categoryId);
        Task<bool> Delete(int categoryId);
        Task<CategoryToReturnDto?> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto);
    }
}
