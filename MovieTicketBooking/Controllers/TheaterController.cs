using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Dtos;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private readonly ITheaterRepository _theaterRepository;

        public TheaterController(ITheaterRepository theaterRepository )
        {
            _theaterRepository = theaterRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Addtheater([FromBody] TheaterDto theaterDto)
        {
            var theater = new Theater()
            {
                Theatre_Name = theaterDto.Theatre_Name,
                Location = theaterDto.Location,
                Capacity = theaterDto.Capacity
            };

            await _theaterRepository.AddTheater(theater);

            return Ok(theater);
        }

        [HttpGet]
        public async Task<IActionResult> ListTheater()
        {
            var data = await _theaterRepository.ListTheater();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updatetheater(int id, [FromBody] TheaterDto theaterDto)
        {
            var theater = new Theater()
            {
                Theatre_Id = id,
                Theatre_Name = theaterDto.Theatre_Name,
                Location = theaterDto.Location,
                Capacity = theaterDto.Capacity,
            };

            var updatedtheater = await _theaterRepository.Update(theater);

            if (updatedtheater == null)
            {
                return NotFound();
            }

            return Ok(updatedtheater);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var TheaterToDelete = new Theater() { Theatre_Id = id };

            var deletedTheater = await _theaterRepository.Delete(TheaterToDelete);

            if (deletedTheater == null)
            {
                return NotFound();
            }

            return Ok(deletedTheater);
        }
    }
}
