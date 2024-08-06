using AutoMapper;
using Shaghaf.Core;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Core.Specifications.Home_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Service
{
    public class BirthDayService : IBirthDayService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

      
        public BirthDayService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BirthDayToCreateDto?> CreateBirthDayAsync(BirthDayToCreateDto birthdayDto)
        {
            try
            {
                if (birthdayDto is not null)
                {
                    var birthday = _mapper.Map<Birthday>(birthdayDto);
                    _unitOfWork.Repository<Birthday>().Add(birthday);
                    await _unitOfWork.CompleteAsync();
                    return _mapper.Map<BirthDayToCreateDto>(birthday);
                }
                return null;
            }
            catch (AutoMapperMappingException ex)
            {
                // Log the exception details here if needed
                Console.WriteLine($" Exception: {ex.Message}");
                return null; // Return null or a custom DTO indicating the failure
            }
        }

        public async Task<bool> Delete(int birthdayId)
        {
            var isDeleted = false;

            var birthday = await _unitOfWork.Repository<Birthday>().GetByIdAsync(birthdayId);

            if (birthday is null)
                return isDeleted;


            _unitOfWork.Repository<Birthday>().Delete(birthday);

            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows > 0)
            {

                isDeleted = true;

            }
            return isDeleted;
        }

      

        public async Task<BirthdayDto?> GetBirthDayDetailsAsync(int birthdayId)
        {
            var spec = new BirthdaySpecs(birthdayId);
            var birthday = await _unitOfWork.Repository<Birthday>().GetByIdWithSpecAsync(spec);
            return _mapper.Map<BirthdayDto>(birthday);
        }



        public async Task<BirthDayToCreateDto?> UpdateBirthDayAsync(int id, BirthDayToCreateDto birthdayDto)
        {
            // Correctly retrieve the Birthday entity
            var birthday = await _unitOfWork.Repository<Birthday>().GetByIdAsync(id);
            if (birthday is null)
            {
                return null;
            }
            
            // Directly update the entity's properties
            if (birthdayDto != null)
            {
                //var birthdayEntity = _mapper.Map<Birthday>(birthdayDto);
                birthday.Date = birthdayDto.Date;
                birthday.Description = birthdayDto.Description;
                birthday.HomeId = birthdayDto.HomeId;
                birthday.Name = birthdayDto.Name ;

                // No need to map the entity again; just save it
                _unitOfWork.Repository<Birthday>().Update(birthday);
                try
                {
                    // Attempt to complete the unit of work
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    Console.WriteLine($"Failed to complete unit of work: {ex.Message}");
                    throw; // Rethrow the exception to handle it further up the call stack
                }


                // Return the updated entity mapped to BirthDayToCreateDto
                return _mapper.Map<BirthDayToCreateDto>(birthday);
            }

            return null;
        }
    }
}
