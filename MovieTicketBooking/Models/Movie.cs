using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class Movie
    {
        [Key]
        public int Movie_Id { get; set; }
        public string Movie_Name { get; set; }
        public string Director { get; set; }
        public string genre { get; set; }
        public int Duration { get; set; }
        public DateTime Relase_Date { get; set; }
        public string Poster_Url { get; set; }
        public string Trailer_Url { get; set; }


    }
}
