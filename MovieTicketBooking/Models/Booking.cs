using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public  User User { get; set; }
        public int ShowId { get; set; }
        [ForeignKey("ShowId")]
        public Show Show { get; set; }
        public int NumTickets { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingTime { get; set; }
    }
}
