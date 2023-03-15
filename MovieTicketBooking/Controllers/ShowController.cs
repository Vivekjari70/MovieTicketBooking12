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
    public class ShowController : ControllerBase
    {
        private readonly IShowRepository _showRepository;

        public ShowController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Addshow([FromBody] ShowDto showDto)
        {
            var show = new Show()
            {
                MovieId = showDto.MovieId,
                TheatreId = showDto.TheatreId,
                StartTime = showDto.StartTime,
                EndTime = showDto.EndTime,
                Price = showDto.Price
            };

            await _showRepository.Addshow(show);

            return Ok(show);
        }

        [HttpGet]
        public async Task<IActionResult> Listshow()
        {
            var data = await _showRepository.Listshow();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updateshow(int id, [FromBody] ShowDto showDto)
        {
            var show = new Show()
            {
                ShowId = id,
                MovieId = showDto.MovieId,
                TheatreId = showDto.TheatreId,
                StartTime = showDto.StartTime,
                EndTime = showDto.EndTime,
                Price = showDto.Price
            };

            var updatedshow = await _showRepository.Update(show);

            if (updatedshow == null)
            {
                return NotFound();
            }

            return Ok(updatedshow);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteshow(int id)
        {
            var ShowToDelete = new Show() { ShowId = id };

            var deletedShow = await _showRepository.Delete(ShowToDelete);

            if (deletedShow == null)
            {
                return NotFound();
            }

            return Ok(deletedShow);
        }

    }
}
