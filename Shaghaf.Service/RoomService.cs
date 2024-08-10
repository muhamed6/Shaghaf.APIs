using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shaghaf.Core;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities;
using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Entities.HomeEntities;
using Shaghaf.Core.Entities.RoomEntities;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Core.Specifications.Home_Specs;
using Shaghaf.Infrastructure.Data;
using Stripe;
using Stripe.Climate;
using Stripe.Terminal;
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
        private readonly StoreContext _context;

        public RoomService(IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<RoomService> logger,
            StoreContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<RoomDto?> CreateRoomAsync(RoomToCreateOrUpdateDto model)
        {
            var room = new Room();

            if (model is not null)
            {

                room.Price = model.Price;
                room.Date = model.Date;
                room.LocationId = model.LocationId;
                room.Description = model.Description;
                room.Seat = model.Seat;
                room.Name = model.Name;
                room.Rate = model.Rate;
                room.Offer = model.Offer;
                try
                {
                    room.Plan = (RoomPlan)Enum.Parse(typeof(RoomPlan), model.Plan);
                    room.Type = (RoomType)Enum.Parse(typeof(RoomType), model.Type);

                    room.RoomCategories = model.SelectedCategories.Select(c => new RoomCategory { CategoryId = c }).ToList();
                }
                catch (ArgumentException ex)
                {

                    return null;
                }


                _unitOfWork.Repository<Room>().Add(room);
                try
                {

                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {

                    return null;
                }

                return _mapper.Map<RoomDto>(room);
            } 
            return null;
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
            var spec = new RoomSpecs();

            var rooms = await roomRepo.GetAllWithSpecAsync(spec);

            var roomslist = rooms.ToList();

            return roomslist;
        }

        public async Task<Room?> GetRoomById(int roomId)
        {
            var roomRepo = _unitOfWork.Repository<Room>();
            var spec = new RoomSpecs(roomId);

            var room = await roomRepo.GetByIdWithSpecAsync(spec);

            if (room is null)
                return null;
            return room;
        }

        

        public async Task<RoomToCreateOrUpdateDto?> UpdateRoomAsync(int roomId, RoomToCreateOrUpdateDto roomDto)
        {
            var room = _context.Rooms
                .Include(g => g.RoomCategories)
                .SingleOrDefault(g => g.Id == roomId);
            if (room is null)
            {
                return null;
            }

            if (roomDto is not null)
            {
                room.Date = roomDto.Date;
                room.Name = roomDto.Description;
                room.Offer = roomDto.Offer;
                room.Rate = roomDto.Rate;
                room.Seat = roomDto.Seat;
                room.Description = roomDto.Description;
                room.LocationId = roomDto.LocationId;
                room.Price = roomDto.Price;

                try
                {
                    room.Plan = (RoomPlan)Enum.Parse(typeof(RoomPlan), roomDto.Plan);
                    room.Type = (RoomType)Enum.Parse(typeof(RoomType), roomDto.Type);

                    // Handle RoomCategory updates
                    var currentRoomCategories = room.RoomCategories.ToList();
                    var selectedCategories = roomDto.SelectedCategories;

                    // Remove existing associations
                    foreach (var category in currentRoomCategories)
                    {
                        if (!selectedCategories.Contains(category.CategoryId))
                        {
                            room.RoomCategories.Remove(category);
                        }
                    }

                    // Add new associations
                    foreach (var categoryId in selectedCategories)
                    {
                        if (!currentRoomCategories.Any(c => c.CategoryId == categoryId))
                        {
                            room.RoomCategories.Add(new RoomCategory { CategoryId = categoryId });
                        }
                    }

                    _unitOfWork.Repository<Room>().Update(room);
                }
                catch (ArgumentException ex)
                {
                    return null;
                }

                try
                {
                    await _unitOfWork.CompleteAsync();
                }
                catch (Exception ex)
                {
                    return null;
                }

                return _mapper.Map<RoomToCreateOrUpdateDto>(room);
            }

            return null;
        }

    }
}
