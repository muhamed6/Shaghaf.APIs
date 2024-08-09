using AutoMapper;
using Shaghaf.Core;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Services.Contract;
using Stripe.Terminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Service
{
    public class PhotoSessionService : IPhotoSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PhotoSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PhotoSessionDto?> CreatePhotoSessionAsync(PhotoSessionDto photoSessionDto)
        {
           
                if (photoSessionDto is not null)
                {
                    var photoSession = _mapper.Map<PhotoSession>(photoSessionDto);
                    _unitOfWork.Repository<PhotoSession>().Add(photoSession);

                try
                {
                  await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {

                    return null;
                }


                    return _mapper.Map<PhotoSessionDto>(photoSession);
                }
                return null;
            
            
        }

        public async Task<bool> Delete(int photoSessionId)
        {
            var isDeleted = false;

            var photoSession = await _unitOfWork.Repository<PhotoSession>().GetByIdAsync(photoSessionId);

            if (photoSession is null)
                return isDeleted;


            _unitOfWork.Repository<PhotoSession>().Delete(photoSession);

            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows > 0)
            {

                isDeleted = true;

            }
            return isDeleted;
        }

        public async Task<IReadOnlyList<PhotoSessionDto>> GetAllPhotoSessionsAsync()
        {

            var photoSessions = await _unitOfWork.Repository<PhotoSession>().GetAllAsync();
            return _mapper.Map<IReadOnlyList<PhotoSessionDto>>(photoSessions);

        }

        public async Task<PhotoSessionDto?> GetPhotoSessionDetailsAsync(int photoSessionId)
        {


            var photoSession = await  _unitOfWork.Repository<PhotoSession>().GetByIdAsync(photoSessionId);

            if (photoSession is null)
                return null;
            return _mapper.Map<PhotoSessionDto> (photoSession);
        }

        public async Task<PhotoSessionDto?> UpdatePhotoSessionAsync(int photoSessionId, PhotoSessionDto photoSessionDto)
        {
            var photoSession = await _unitOfWork.Repository<PhotoSession>().GetByIdAsync(photoSessionId);
            if (photoSession is null)
            {
                return null;
            }


            if (photoSessionDto is not null)
            {

                photoSession.RoomId = photoSessionDto.RoomId;
                photoSession.Price = photoSessionDto.Price;
                photoSession.Description = photoSessionDto.Description;
                photoSession.Date = photoSessionDto.Date;


                _unitOfWork.Repository<PhotoSession>().Update(photoSession);
                try
                {

                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }


                return _mapper.Map<PhotoSessionDto>(photoSession);
            }

            return null;
        }
    }
}
