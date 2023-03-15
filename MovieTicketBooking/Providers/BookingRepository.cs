using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Providers
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationContext _applicationContext;

        public BookingRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Booking> AddBooking(Booking booking)
        {
            var userObj = await _applicationContext.Bookingss.AddAsync(booking);
            await _applicationContext.SaveChangesAsync();
            return userObj.Entity; 
        }

        public async Task<Show> GetShowById(int showId)
        {
            return await _applicationContext.Shows.FirstOrDefaultAsync(x=>x.ShowId==showId);
        }

        public async Task<List<Booking>> ListBooking()
        {
            return await _applicationContext.Bookingss.ToListAsync();
        }
        public async Task<Booking> Update(Booking booking)
        {
            var existingBooking = await _applicationContext.Bookingss.FirstOrDefaultAsync(u => u.BookingId == booking.BookingId);

            if (existingBooking == null)
            {
                return null;
            }

            existingBooking.UserId = booking.UserId;
            existingBooking.ShowId = booking.ShowId;
            existingBooking.NumTickets = booking.NumTickets;
            existingBooking.BookingTime = booking.BookingTime;
            
            await _applicationContext.SaveChangesAsync();

            return existingBooking;
        }

        public async Task<Booking> Delete(Booking booking)
        {
            var existingBooking = await _applicationContext.Bookingss.FirstOrDefaultAsync(u => u.BookingId == booking.BookingId);

            if (existingBooking == null)
            {
                return null;
            }

            _applicationContext.Bookingss.Remove(existingBooking);

            await _applicationContext.SaveChangesAsync();

            return existingBooking;
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        
    }
}
