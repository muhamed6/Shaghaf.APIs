using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IDecorationService
    {
        Task<DecorationDto?> CreateDecorationAsync(DecorationDto decorationDto);
        Task<DecorationDto?> UpdateDecorationAsync(int decorationId, DecorationDto decorationDto);

        Task<Decoration?> GetDecorationDetailsAsync(int decorationId);

        Task<IReadOnlyList<DecorationDto>> GetAllDecorationsAsync();

        Task<bool> Delete(int decorationId);
    }
}
