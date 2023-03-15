using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Interfaces
{
    public interface IBookingRepository : ICommonRepository
    {
        Task<List<Booking>> ListBooking();
        Task<Booking> AddBooking(Booking booking);

        Task<Show> GetShowById(int showId);
        Task<Booking> Update(Booking booking);
        Task<Booking> Delete(Booking booking);
    }
}
