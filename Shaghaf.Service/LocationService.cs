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
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationService(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Location?> CreateLocationAsync(LocationDto locationDto)
        {
            if (locationDto is not null)
            {
                var location = _mapper.Map<Location>(locationDto);
                _unitOfWork.Repository<Location>().Add(location);
                await _unitOfWork.CompleteAsync();
                return (location);
            }
            return null;
        }

        public async Task<bool> Delete(int locationId)
        {
            var isDeleted = false;

            var location = await _unitOfWork.Repository<Location>().GetByIdAsync(locationId);

            if (location is null)
                return isDeleted;


            _unitOfWork.Repository<Location>().Delete(location);

            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows > 0)
            {

                isDeleted = true;

            }
            return isDeleted;
        }

        public async Task<IReadOnlyList<Location>> GetAllLocationsAsync()
        {
            var locations = await _unitOfWork.Repository<Location>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<Location>>(locations);
        }

        public async Task<Location?> GetLocationDetailsAsync(int locationId)
        {
            var location = await _unitOfWork.Repository<Location>().GetByIdAsync(locationId);
            return (location);
        }

        public async Task<Location?> UpdateLocationAsync(int locationId, LocationDto locationDto)
        {
            var location = await _unitOfWork.Repository<Location>().GetByIdAsync(locationId);
            if (location is null)
            {
                return null;
            }


            if (locationDto is not null)
            {

                location.Street = locationDto.Street;
                location.City = locationDto.City;
                location.Country = locationDto.Country;


                _unitOfWork.Repository<Location>().Update(location);
                try
                {

                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }


                return (location);
            }

            return null;
        }
    }
}
