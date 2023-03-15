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
    public class TheaterRepository : ITheaterRepository
    {
        private readonly ApplicationContext _applicationContext;

        public TheaterRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }
        public async Task<Theater> AddTheater(Theater theater)
        {
            var userObj = await _applicationContext.Theaters.AddAsync(theater);
            await _applicationContext.SaveChangesAsync();
            return userObj.Entity;
        }

        public async Task<List<Theater>> ListTheater()
        {
            return await _applicationContext.Theaters.ToListAsync();
        }

        public async Task<Theater> Update(Theater theater)
        {
            var existingTheater = await _applicationContext.Theaters.FirstOrDefaultAsync(u => u.Theatre_Id == theater.Theatre_Id);

            if (existingTheater == null)
            {
                return null;
            }

            existingTheater.Theatre_Name = theater.Theatre_Name;
            existingTheater.Location = theater.Location;
            existingTheater.Capacity = theater.Capacity;


            await _applicationContext.SaveChangesAsync();

            return existingTheater;
        }
        public async Task<Theater> Delete(Theater theater)
        {
            var existingTheater = await _applicationContext.Theaters.FirstOrDefaultAsync(u => u.Theatre_Id == theater.Theatre_Id);

            if (existingTheater == null)
            {
                return null;
            }

            _applicationContext.Theaters.Remove(existingTheater);

            await _applicationContext.SaveChangesAsync();

            return existingTheater;
        }

        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
