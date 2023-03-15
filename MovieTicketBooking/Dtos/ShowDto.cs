using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Dtos
{
    public class ShowDto
    {
        public int MovieId { get; set; }
        public int TheatreId { get; set; }
        public int Price { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
