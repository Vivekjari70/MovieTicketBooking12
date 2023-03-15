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
    public class ShowRepository : IShowRepository
    {
        private readonly ApplicationContext _applicationContext;

        public ShowRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<Show> Addshow(Show show)
        {
            var userObj = await _applicationContext.Shows.AddAsync(show);
            await _applicationContext.SaveChangesAsync();
            return userObj.Entity;
        }

        public async Task<List<Show>> Listshow()
        {
            return await _applicationContext.Shows.ToListAsync();
        }

        public async Task<Show> Update(Show show)
        {
            var existingShow = await _applicationContext.Shows.FirstOrDefaultAsync(u => u.ShowId == show.ShowId);

            if (existingShow == null)
            {
                return null;
            }

            existingShow.MovieId = show.MovieId;
            existingShow.TheatreId = show.TheatreId;
            existingShow.StartTime = show.StartTime;
            existingShow.EndTime = show.EndTime;
            existingShow.Price = show.Price;

            await _applicationContext.SaveChangesAsync();

            return existingShow;
        }
        public async Task<Show> Delete(Show show)
        {
            var existingShow = await _applicationContext.Shows.FirstOrDefaultAsync(u => u.ShowId == show.ShowId);

            if (existingShow == null)
            {
                return null;
            }

            _applicationContext.Shows.Remove(existingShow);

            await _applicationContext.SaveChangesAsync();

            return existingShow;
        }
        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        
    }
}
