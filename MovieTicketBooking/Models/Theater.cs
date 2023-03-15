using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Models
{
    public class Theater
    {
        [Key]
        public int Theatre_Id { get; set; }
        public string Theatre_Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }
}
