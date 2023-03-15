using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> ListBooking()
        {
            var data = await _bookingRepository.ListBooking();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking([FromBody] BookingDto bookingDto)
        {

            var showDetail = await _bookingRepository.GetShowById(bookingDto.ShowId);


            var booking = new Booking()
            {
                UserId = bookingDto.UserId,
                ShowId = bookingDto.ShowId,
                NumTickets = bookingDto.NumTickets,
                TotalPrice = (decimal)(showDetail.Price * bookingDto.NumTickets),
                BookingTime = bookingDto.BookingTime
                
            };

            await _bookingRepository.AddBooking(booking);

            return Ok(new {
                UserId = booking.UserId,
                ShowId = booking.ShowId,
                NumTickets= booking.NumTickets,
                TotalPrice = booking.TotalPrice,
                BookingTime = booking.BookingTime
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] BookingDto bookingDto)
        {

            var showDetail = await _bookingRepository.GetShowById(bookingDto.ShowId);

            var booking = new Booking()
            {
                BookingId = id,
                UserId = bookingDto.UserId,
                ShowId = bookingDto.ShowId,
                NumTickets = bookingDto.NumTickets,
                TotalPrice = (decimal)(showDetail.Price * bookingDto.NumTickets),
                BookingTime = bookingDto.BookingTime
            };

            var updatedBooking = await _bookingRepository.Update(booking);

            if (updatedBooking == null)
            {
                return NotFound();
            }

            return Ok(updatedBooking);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var BookingToDelete = new Booking() { BookingId = id };

            var deletedBooking = await _bookingRepository.Delete(BookingToDelete);

            if (deletedBooking == null)
            {
                return NotFound();
            }

            return Ok(deletedBooking);
        }
    }
}
