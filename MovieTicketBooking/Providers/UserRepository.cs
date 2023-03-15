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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _applicationContext;

        public UserRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<User> Register(User user)
        {
            var userObj = await _applicationContext.Users.AddAsync(user);
            await _applicationContext.SaveChangesAsync();
            return userObj.Entity;
        }

        public async Task<List<User>>ListUser()
        {
            return await _applicationContext.Users.ToListAsync();
        }

        public async Task<User> Update(User user)
        {
            var existingUser = await _applicationContext.Users.FirstOrDefaultAsync(u => u.User_Id == user.User_Id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            await _applicationContext.SaveChangesAsync();

            return existingUser;
        }
        public async Task<User> Delete(User user)
        {
            var existingUser = await _applicationContext.Users.FirstOrDefaultAsync(u => u.User_Id == user.User_Id);

            if (existingUser == null)
            {
                return null;
            }

            _applicationContext.Users.Remove(existingUser);

            await _applicationContext.SaveChangesAsync();

            return existingUser;
        }
        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
           return await _applicationContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
