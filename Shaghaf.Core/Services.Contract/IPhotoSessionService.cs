using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IPhotoSessionService
    {
        Task<PhotoSessionDto?> CreatePhotoSessionAsync(PhotoSessionDto photoSessionDto);
        Task<PhotoSessionDto?> UpdatePhotoSessionAsync(int photoSessionId, PhotoSessionDto photoSessionDto);

        Task<PhotoSessionDto?> GetPhotoSessionDetailsAsync(int photoSessionId);
        Task<IReadOnlyList<PhotoSessionDto>> GetAllPhotoSessionsAsync();

        Task<bool> Delete(int photoSessionId);
    }
}
