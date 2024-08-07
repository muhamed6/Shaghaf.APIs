using Shaghaf.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface ICakeService
    {
        Task<CakeDto?> CreateCakeAsync(CakeDto cakeDto);
        Task<CakeDto?> UpdateCakeAsync(int cakeId, CakeDto cakeDto);

        Task<CakeDto?> GetCakeDetailsAsync(int cakeId);

        Task<IReadOnlyList<CakeDto>> GetAllCakesAsync();

        Task<bool> Delete(int cakeId);
    }
}
