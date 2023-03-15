using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Interfaces
{
    public interface IMovieRepository:ICommonRepository
    {
        Task<Movie> Addmovie(Movie movie);
        Task<List<Movie>> ListMovie();
        Task<Movie> Update(Movie movie);
        Task<Movie> Delete(Movie movie);
        Task<Movie> GetMovieByMovie_Name(string movie_name);
    }
}
