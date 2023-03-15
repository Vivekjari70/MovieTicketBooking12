using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Interfaces
{
    public interface ICommonRepository
    {
        Task SaveChanges();
    }
}
