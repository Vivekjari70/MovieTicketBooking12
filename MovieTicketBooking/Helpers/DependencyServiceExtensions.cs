using Microsoft.Extensions.DependencyInjection;
using MovieTicketBooking.Interfaces;
using MovieTicketBooking.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Helpers
{
    public static class DependencyServiceExtensions
    {
        public static IServiceCollection AddDependencyService(this IServiceCollection services)
        {
            //services.AddSingleton(provide =>)
                services.AddTransient<IUserRepository, UserRepository>();
                services.AddTransient<IMovieRepository, MovieRespository>();
                services.AddTransient<ITheaterRepository, TheaterRepository>();
                services.AddTransient<IShowRepository, ShowRepository>();
                services.AddTransient<IBookingRepository, BookingRepository>();

            return services;

        }
    }
}
