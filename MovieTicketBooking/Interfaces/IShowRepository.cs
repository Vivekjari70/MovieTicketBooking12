using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Interfaces
{
    public interface IShowRepository
    {
        Task<Show> Addshow(Show show);
        Task<List<Show>> Listshow();
        Task<Show> Update(Show show);
        Task<Show> Delete(Show show);
    }
}
