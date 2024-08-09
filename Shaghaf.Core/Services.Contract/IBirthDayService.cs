using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IBirthDayService
    {
        Task<BirthDayToCreateDto?> CreateBirthDayAsync(BirthDayToCreateDto birthdayDto);
        Task<BirthDayToCreateDto?> UpdateBirthDayAsync(int id, BirthDayToCreateDto birthdayDto); 

        Task<Birthday?> GetBirthDayDetailsAsync(int birthdayId);
        Task<IReadOnlyList<Birthday>> GetAllBirthDaysAsync();

        Task<bool> Delete(int bookingId);
    }
}
