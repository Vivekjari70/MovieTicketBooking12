using Microsoft.EntityFrameworkCore;
using MovieTicketBooking.Data;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Providers
{
    public class MovieRespository: IMovieRepository
    {

        private readonly ApplicationContext _applicationContext;

        public MovieRespository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<Movie> Addmovie(Movie movie)
        {
            var userObj = await _applicationContext.Movies.AddAsync(movie);
            await _applicationContext.SaveChangesAsync();
            return userObj.Entity;
        }
        public async Task<List<Movie>> ListMovie()
        {
            return await _applicationContext.Movies.ToListAsync();
        }

        public async Task<Movie> Update(Movie movie)
        {
            var existingMovie = await _applicationContext.Movies.FirstOrDefaultAsync(u => u.Movie_Id == movie.Movie_Id);

            if (existingMovie == null)
            {
                return null;
            }

            existingMovie.Movie_Name = movie.Movie_Name;
            existingMovie.Director = movie.Director;
            existingMovie.Duration = movie.Duration;
            existingMovie.Relase_Date = movie.Relase_Date;
            existingMovie.Poster_Url = movie.Poster_Url;
            existingMovie.Trailer_Url = movie.Trailer_Url;
            

            await _applicationContext.SaveChangesAsync();

            return existingMovie;
        }
        public async Task<Movie> Delete(Movie movie)
        {
            var existingMovie = await _applicationContext.Movies.FirstOrDefaultAsync(u => u.Movie_Id == movie.Movie_Id);

            if (existingMovie == null)
            {
                return null;
            }

            _applicationContext.Movies.Remove(existingMovie);

            await _applicationContext.SaveChangesAsync();

            return existingMovie;
        }
        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetMovieByMovie_Name(string movie_name)
        {
            return await _applicationContext.Movies.FirstOrDefaultAsync(x => x.Movie_Name == movie_name);
        }
        
    }
}
