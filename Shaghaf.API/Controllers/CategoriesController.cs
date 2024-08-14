using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Service;
using Talabat.APIs.Controllers;

namespace Shaghaf.API.Controllers
{

    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategories();
            if (categories.Count < 1)
            {
                return NotFound("There is no category!!");
            }
            return Ok(categories);
        }


        [HttpGet("{categoryId}")]
        [ProducesResponseType(typeof(CategoryToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryToReturnDto?>> GetCategoryById(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);

            if (category is null)
                return NotFound("This Category Does not exist!!");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category?>> CreateCategory(CategoryDto categoryDto)
        {
            var category = await _categoryService.CreateCategoryAsync(categoryDto);

            if (category is null)
                return BadRequest("Invalid Create!!");

            return Ok(category);
        }


        [HttpPost("categoryId")]
        public async Task<ActionResult<Category?>> UpdateCategory(int categoryId, [FromBody] CategoryDto categoryDto)
        {

            var result = await _categoryService.UpdateCategoryAsync(categoryId, categoryDto);

            if (result is null)
            {
                return BadRequest("Invalid Data!!");

            }
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var isDeleted = await _categoryService.Delete(categoryId);
            return isDeleted ? Ok("Deleted") : BadRequest("Invalid Operation!");
        }
    }
}
