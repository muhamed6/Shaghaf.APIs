using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface ILocationService
    {
        Task<Location?> CreateLocationAsync(LocationDto locationDto);
        Task<Location?> UpdateLocationAsync(int locationId, LocationDto locationDto);

        Task<Location?> GetLocationDetailsAsync(int locationId);

        Task<IReadOnlyList<Location>> GetAllLocationsAsync();

        Task<bool> Delete(int locationId);

    }
}
