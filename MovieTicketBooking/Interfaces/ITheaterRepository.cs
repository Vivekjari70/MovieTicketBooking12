using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Interfaces
{
   public  interface ITheaterRepository: ICommonRepository
    {
        Task<Theater> AddTheater(Theater theater);
        Task<List<Theater>> ListTheater();
        Task<Theater> Update(Theater theater);
        Task<Theater> Delete(Theater theater);
    }
}
