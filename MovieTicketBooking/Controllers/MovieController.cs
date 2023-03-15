using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Dtos;
using MovieTicketBooking.Helpers;
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
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddMovie([FromBody] MovieDto movieDto)
        {

            var MovieDetails = await _movieRepository.GetMovieByMovie_Name(movieDto.Movie_Name);

            if (MovieDetails.Movie_Name != null)
            {
                return BadRequest(new ResponseModel
                {
                    ErrorCode = "U002",
                    Message = ErrorMessage.U002
                });
            }

            var movie = new Movie()
            {
                Movie_Name = movieDto.Movie_Name,
                Director = movieDto.Director,
                genre = movieDto.genre,
                Duration = movieDto.Duration,
                Relase_Date = movieDto.Relase_Date,
                Poster_Url = movieDto.Poster_Url,
                Trailer_Url = movieDto.Trailer_Url
            };

            await _movieRepository.Addmovie(movie);

            return Ok(movie);
        }
        [HttpGet]
        public async Task<IActionResult> ListMovie()
        {
            var data = await _movieRepository.ListMovie();
            return Ok(data);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieDto movieDto)
        {
            var movie = new Movie()
            {
                Movie_Id = id,
                Movie_Name = movieDto.Movie_Name,
                Director = movieDto.Director,
                Duration = movieDto.Duration,
                Relase_Date = movieDto.Relase_Date,
                Poster_Url = movieDto.Poster_Url,
                Trailer_Url = movieDto.Trailer_Url
            };

            var updatedmovie = await _movieRepository.Update(movie);

            if (updatedmovie == null)
            {
                return NotFound();
            }

            return Ok(updatedmovie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var MovieToDelete = new Movie() { Movie_Id = id };

            var deletedMovie = await _movieRepository.Delete(MovieToDelete);

            if (deletedMovie == null)
            {
                return NotFound();
            }

            return Ok(deletedMovie);
        }
    }
}
