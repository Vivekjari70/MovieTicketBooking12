using MovieTicketBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTicketBooking.Interfaces
{
    public interface IUserRepository : ICommonRepository
    {
        Task<User> Register(User user);
        Task<List<User>> ListUser();
        Task<User> Update(User user);
        Task<User> Delete(User user);
        Task<User> GetUserByEmail(string email);
    }
}
