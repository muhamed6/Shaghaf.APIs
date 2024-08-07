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
    public class DecorationService : IDecorationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DecorationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<DecorationDto?> CreateDecorationAsync(DecorationDto decorationDto)
        {
            if (decorationDto is not null)
            {
                var decoration = _mapper.Map<Decoration>(decorationDto);
                _unitOfWork.Repository<Decoration>().Add(decoration);
                try
                {

                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {

                    return null;
                }
                return _mapper.Map<DecorationDto>(decoration);
            }
            return null;
        }



        public async Task<IReadOnlyList<DecorationDto>> GetAllDecorationsAsync()
        {
            var decorations = await _unitOfWork.Repository<Decoration>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<DecorationDto>>(decorations);
        }

        public async Task<Decoration?> GetDecorationDetailsAsync(int decorationId)
        {

            var decoration = await _unitOfWork.Repository<Decoration>().GetByIdAsync(decorationId);

            if (decoration is not null)
                return decoration;

            return null;
        }

        public async Task<DecorationDto?> UpdateDecorationAsync(int decorationId, DecorationDto decorationDto)
        {
            var decoration = await _unitOfWork.Repository<Decoration>().GetByIdAsync(decorationId);
            if (decoration is null)
            {
                return null;
            }


            if (decorationDto is not null)
            {

                decoration.BirthdayId = decorationDto.BirthdayId;
                decoration.Price = decorationDto.Price;
                decoration.Description = decorationDto.Description;
                

                _unitOfWork.Repository<Decoration>().Update(decoration);
                try
                {

                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }


                return _mapper.Map<DecorationDto>(decoration);
            }

            return null;
        }


        public async Task<bool> Delete(int decorationId)
        {
            var isDeleted = false;

            var decoration = await _unitOfWork.Repository<Decoration>().GetByIdAsync(decorationId);

            if (decoration is null)
                return isDeleted;


            _unitOfWork.Repository<Decoration>().Delete(decoration);

            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows > 0)
            {

                isDeleted = true;

            }
            return isDeleted;
        }
    }
}
