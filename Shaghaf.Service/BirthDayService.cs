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
            
                if (birthdayDto is not null)
                {
                    var birthday = _mapper.Map<Birthday>(birthdayDto);
                    _unitOfWork.Repository<Birthday>().Add(birthday);
                try
                {
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {

                    return null;
                }
                    return _mapper.Map<BirthDayToCreateDto>(birthday);
                }
                return null;
            
           
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

        public async Task<IReadOnlyList<Birthday>> GetAllBirthDaysAsync()
        {
            var spec = new BirthdaySpecs();
            var birthdays = await _unitOfWork.Repository<Birthday>().GetAllWithSpecAsync(spec);
            return (birthdays);
        }

        public async Task<Birthday?> GetBirthDayDetailsAsync(int birthdayId)
        {
            var spec = new BirthdaySpecs(birthdayId);
            var birthday = await _unitOfWork.Repository<Birthday>().GetByIdWithSpecAsync(spec);
            return (birthday);
        }



        public async Task<BirthDayToCreateDto?> UpdateBirthDayAsync(int birthdayId, BirthDayToCreateDto birthdayDto)
        {
            
            var birthday = await _unitOfWork.Repository<Birthday>().GetByIdAsync(birthdayId);
            if (birthday is null)
            {
                return null;
            }
            
            
            if (birthdayDto != null)
            {
                
                birthday.Date = birthdayDto.Date;
                birthday.Description = birthdayDto.Description;
                birthday.Name = birthdayDto.Name ;
                birthday.RoomId = birthdayDto.RoomId;
             
                _unitOfWork.Repository<Birthday>().Update(birthday);
                try
                {
                   
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }


                return _mapper.Map<BirthDayToCreateDto>(birthday);
            }

            return null;
        }
    }
}
