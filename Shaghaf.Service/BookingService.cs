using AutoMapper;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.BookingEntities;
using Shaghaf.Core.Repositories.Contract;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Core.Specifications.Booking_Spec;
using Shaghaf.Core;
using Shaghaf.Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

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
            // Log the exception details here if needed
            Console.WriteLine($" Exception: {ex.Message}");
            return null; // Return null or a custom DTO indicating the failure
        }

     
    }



    public async Task UpdateBookingAsync(BookingDto bookingDto)
    {
        var booking = await _bookingRepository.FindUniqueBookingAsync(
            bookingDto.RoomId, bookingDto.StartDate, bookingDto.EndDate, bookingDto.CustomerName);

        if (booking == null)
        {
            throw new KeyNotFoundException("No booking found matching the specified criteria.");
        }

        if (Enum.TryParse<BookingStatus>(bookingDto.Status, out var status))
        {
            booking.Status = status;
        }
        else
        {
            throw new ArgumentException("Invalid status value");
        }

        _bookingRepository.Update(booking);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<BookingDto?> GetBookingDetailsAsync(int bookingId)
    {
        var spec = new BookWithAdditionalItemsSpecs(bookingId);
        var booking = await _unitOfWork.Repository<Booking>().GetByIdWithSpecAsync(spec);
        return booking == null ? null : _mapper.Map<BookingDto>(booking);
    }

    public async Task<IReadOnlyList<BookingDto>> GetAllBookingDetailsAsync()
    {
        var spec = new BookWithAdditionalItemsSpecs();
        var bookings = await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);
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
}
