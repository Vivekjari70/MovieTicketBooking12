using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Dtos
{
    public class BookingDto
    {
        public int UserId { get; set; }
        public int ShowId { get; set; }      
        public int NumTickets { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
