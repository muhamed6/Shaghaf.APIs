﻿using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.RoomEntities;
using Stripe.Climate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shaghaf.Core.Services.Contract
{
    public interface IRoomService
    {
        Task<RoomDto?> CreateRoomAsync(string phoneNumber, RoomToCreateOrUpdateDto model);
        Task<IReadOnlyList<Room>> GetAllRooms();
        Task<Room?> GetRoomById(int roomId);
        Task<bool> Delete(int roomId);
        Task<RoomToCreateOrUpdateDto?> UpdateRoomAsync(int roomId, string phoneNumber, RoomToCreateOrUpdateDto roomDto);
        Task<IReadOnlyList<Room>> GetRoomsForUserAsync(string phoneNumber);
    }
}
