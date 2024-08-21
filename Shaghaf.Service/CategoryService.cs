using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shaghaf.Core;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Core.Specifications.Home_Specs;
using Shaghaf.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly StoreContext _context;

        public CategoryService(IUnitOfWork unitOfWork,
            IMapper mapper,
            StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
        }




        public async Task<CategoryToReturnDto?> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category();

            if (categoryDto is not null)
            {

                category.ImageUrl = categoryDto.ImageUrl;
                category.Name = categoryDto.Name;
                try
                {
                    category.RoomCategories = categoryDto.SelectedRooms.Select(c => new RoomCategory { RoomId = c }).ToList();
                }
                catch (ArgumentException ex)
                {

                    return null;
                }


                _unitOfWork.Repository<Category>().Add(category);
                try
                {

                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {

                    return null;
                }

                return (_mapper.Map<CategoryToReturnDto>(category));
            }
            return null;
        }

        public async Task<bool> Delete(int categoryId)
        {
            var isDeleted = false;

            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(categoryId);

            if (category is null)
                return isDeleted;


            _unitOfWork.Repository<Category>().Delete(category);

            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows > 0)
            {

                isDeleted = true;

            }
            return isDeleted;
        }

        public async Task<IReadOnlyList<CategoryToReturnDto>> GetAllCategories()
        {
            var categoryRepo = _unitOfWork.Repository<Category>();
            var spec = new CategorySpecs();

            var Categories = await categoryRepo.GetAllWithSpecAsync(spec);

            return _mapper.Map<IReadOnlyList<CategoryToReturnDto>>(Categories);
        }

        public async Task<CategoryToReturnDto?> GetCategoryById(int categoryId)
        {
            var categoryRepo = _unitOfWork.Repository<Category>();
            var spec = new CategorySpecs(categoryId);

            var category = await categoryRepo.GetByIdWithSpecAsync(spec);

            if (category is null)
                return null;
            return _mapper.Map<CategoryToReturnDto>(category);
        }

        public async Task<CategoryToReturnDto?> UpdateCategoryAsync(int categoryId, CategoryDto categoryDto)
        {
            var category = _context.Categories
                .Include(g => g.RoomCategories)
                .SingleOrDefault(g => g.Id == categoryId);
            if (category is null)
            {
                return null;
            }

            if (categoryDto is not null)
            {
                category.ImageUrl = categoryDto.ImageUrl;
                category.Name = categoryDto.Name;
             

                try
                {
                    category.RoomCategories = categoryDto.SelectedRooms.Select(c => new RoomCategory { RoomId = c }).ToList();


                    // Handle RoomCategory updates
                    var currentRoomCategories = category.RoomCategories.ToList();
                    var selectedCategories = categoryDto.SelectedRooms;

                    // Remove existing associations
                    foreach (var room in currentRoomCategories)
                    {
                        if (!selectedCategories.Contains(room.RoomId))
                        {
                            category.RoomCategories.Remove(room);
                        }
                    }

                    // Add new associations
                    foreach (var roomId in selectedCategories)
                    {
                        if (!currentRoomCategories.Any(r => r.RoomId == roomId))
                        {
                            category.RoomCategories.Add(new RoomCategory { RoomId = roomId });
                        }
                    }

                    _unitOfWork.Repository<Category>().Update(category);
                }
                catch (ArgumentException ex)
                {
                    return null;
                }

                try
                {
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }

                return (_mapper.Map<CategoryToReturnDto> (category));
            }

            return null;
        }
    }
}
