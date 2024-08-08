using AutoMapper;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
//using Shaghaf.Core.Specifications.Booking_Spec;
using Shaghaf.Core;
using Shaghaf.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Shaghaf.Core.Entities.HomeEntities;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BookingService(IBookingRepository bookingRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookingDto?> CreateBookingAsync(BookingDto bookingDto)
    {
        try
        {
            if (bookingDto is not null)
            {
                var booking = _mapper.Map<Booking>(bookingDto);
                _unitOfWork.Repository<Booking>().Add(booking);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<BookingDto>(booking);
            }
            return null;
        }
        catch (AutoMapperMappingException ex)
        {
          
            Console.WriteLine($" Exception: {ex.Message}");
            return null; 
        }

     
    }



    public async Task<BookingDto?> GetBookingDetailsAsync(int bookingId)
    {
        //var spec = new BookWithAdditionalItemsSpecs(bookingId);
        //var booking = await _unitOfWork.Repository<Booking>().GetByIdWithSpecAsync(spec);
        var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);
        return booking == null ? null : _mapper.Map<BookingDto>(booking);
    }

    public async Task<IReadOnlyList<BookingDto>> GetAllBookingDetailsAsync()
    {
        //var spec = new BookWithAdditionalItemsSpecs();
        //var bookings = await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);
        var bookings = await _unitOfWork.Repository<Booking>().GetAllAsync();
        return _mapper.Map<IReadOnlyList<BookingDto>>(bookings);
    }

    

    public async Task<bool> Delete(int bookingId)
    {
        var isDeleted = false;

        var booking =await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);

        if (booking is null)
            return isDeleted;


        _unitOfWork.Repository<Booking>().Delete(booking);

        var effectedRows = await _unitOfWork.CompleteAsync();

        if (effectedRows > 0)
        {

            isDeleted = true;

        }
        return isDeleted;
    }

    public async Task<BookingDto?> UpdateBookingAsync(int bookingId, BookingDto bookingDto)
    {
        var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);
        if (booking is null)
        {
            return null;
        }


        if (bookingDto != null)
        {

            booking.RoomId = bookingDto.RoomId;
            booking.StartDate = bookingDto.StartDate;
            booking.EndDate = bookingDto.EndDate;
            booking.CustomerName = bookingDto.CustomerName;
            booking.SeatCount = bookingDto.SeatCount;
            booking.Discount = bookingDto.Discount;
            booking.Amount = bookingDto.Amount;
            booking.Currency = bookingDto.Currency;
            booking.SessionId = bookingDto.SessionId;

            try
            {
                booking.Status = (BookingStatus)Enum.Parse(typeof(BookingStatus), bookingDto.Status);
            }
            catch (ArgumentException ex)
            {
                // Handle the case where the string does not match any enum member 
                Console.WriteLine($"Invalid status value: {bookingDto.Status}");
                throw;
            }



            _unitOfWork.Repository<Booking>().Update(booking);
            try
            {

                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Failed to complete unit of work: {ex.Message}");
                throw;
            }


            return _mapper.Map<BookingDto>(booking);
        }

        return null;
    }
}
