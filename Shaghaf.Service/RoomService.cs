using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shaghaf.Core;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shaghaf.Service
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RoomService> _logger;

        public RoomService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<RoomService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Room?> CreateRoomAsync(RoomToCreateOrUpdateDto model)
        {
            var roomRepo = _unitOfWork.Repository<Room>();
            var room = new Room
            {
                Offer = model.Offer,
                Rate  = model.Rate,
                Name = model.Name,
                Seat = model.Seat,
                Description = model.Description,
                Location = model.Location,
                Date = model.Date,
                Price = model.Price
               
            };
            roomRepo.Add(room);
            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return room;


        }

        public async Task<bool> Delete(int roomId)
        {
            var isDeleted = false;

            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(roomId);

            if (room is null)
                return isDeleted;


            _unitOfWork.Repository<Room>().Delete(room);

            var effectedRows = await _unitOfWork.CompleteAsync();

            if (effectedRows > 0)
            {

                isDeleted = true;

            }
            return isDeleted;
        }

        public async Task<IReadOnlyList<Room>> GetAllRooms()
        {
            var roomRepo = _unitOfWork.Repository<Room>();

            var rooms = await roomRepo.GetAllAsync();

            var roomslist = rooms.ToList();

            return roomslist;
        }

        public async Task<Room?> GetRoomById(int roomId)
        {
            var roomRepo = _unitOfWork.Repository<Room>();

            var room = await roomRepo.GetByIdAsync(roomId);

            if (room is null)
                return null;
            return room;
        }

        public async Task<RoomToCreateOrUpdateDto?> UpdateRoomAsync(int roomId, RoomToCreateOrUpdateDto roomDto)
        {
            var room = await _unitOfWork.Repository<Room>().GetByIdAsync(roomId);
            if (room is null)
            {
                return null;
            }


            if (roomDto != null)
            {

                room.Date = roomDto.Date;
                room.Name = roomDto.Description;
                room.Offer = roomDto.Offer;
                room.Rate = roomDto.Rate;
                room.Seat = roomDto.Seat;
                room.Description = roomDto.Description;
                room.Location = roomDto.Location;
                room.Price = roomDto.Price;

                try
                {
                    room.Plan = (RoomPlan)Enum.Parse(typeof(RoomPlan), roomDto.Plan);
                    room.Type = (RoomType)Enum.Parse(typeof(RoomType), roomDto.Type);
                }
                catch (ArgumentException ex)
                {
                    _logger.LogError(ex, $"Invalid Plan Or Type value: {roomDto.Plan}, {roomDto.Type}");

                    return null;
                }
             


                _unitOfWork.Repository<Room>().Update(room);
                try
                {

                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Failed to complete unit of work: {ex.Message}");
                    throw;
                }


                return _mapper.Map<RoomToCreateOrUpdateDto>(room);
            }

            return null;
        }
    }
}
