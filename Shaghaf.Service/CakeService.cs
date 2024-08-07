using AutoMapper;
using Shaghaf.Core;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Service
{
    public class CakeService : ICakeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CakeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CakeDto?> CreateCakeAsync(CakeDto cakeDto)
        {
            
            
                if (cakeDto is not null)
                {
                    var cake = _mapper.Map<Cake>(cakeDto);
                    _unitOfWork.Repository<Cake>().Add(cake);
                    await _unitOfWork.CompleteAsync();
                    return _mapper.Map<CakeDto>(cake);
                }
                return null;
            
           
        }

        public async Task<bool> Delete(int cakeId)
        {
            var isDeleted = false;

            var cake = await _unitOfWork.Repository<Cake>().GetByIdAsync(cakeId);

            if (cake is null)
                return isDeleted;


            _unitOfWork.Repository<Cake>().Delete(cake);

            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows > 0)
            {

                isDeleted = true;

            }
            return isDeleted;
        }

        public async Task<IReadOnlyList<CakeDto>> GetAllCakesAsync()
        {
            var cakes = await _unitOfWork.Repository<Cake>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<CakeDto>>(cakes);
        }

        public async Task<CakeDto?> GetCakeDetailsAsync(int cakeId)
        {
            var cake = await _unitOfWork.Repository<Cake>().GetByIdAsync(cakeId);
            return _mapper.Map<CakeDto>(cake);
        }

        public async Task<CakeDto?> UpdateCakeAsync(int cakeId, CakeDto cakeDto)
        {
            var cake = await _unitOfWork.Repository<Cake>().GetByIdAsync(cakeId);
            if (cake is null)
            {
                return null;
            }


            if (cakeDto is not null)
            {

                cake.BirthdayId = cakeDto.BirthdayId;
                cake.ServingSize = cakeDto.ServingSize;
                cake.Price = cakeDto.Price;
                cake.Name = cakeDto.Name;


                _unitOfWork.Repository<Cake>().Update(cake);
                try
                {

                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {

                    return null;
                }


                return _mapper.Map<CakeDto>(cake);
            }

            return null;
        }
    }
}
