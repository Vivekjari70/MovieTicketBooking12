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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult>Register([FromBody] UserDto userDto)
        {

            var userDetails = await _userRepository.GetUserByEmail(userDto.Email);

            if(userDetails.Email != null)
            {
                return BadRequest(new ResponseModel
                {
                    ErrorCode = "U001",
                    Message = ErrorMessage.U001
                });
            }


            var user = new User()
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = userDto.Password
            };

            await _userRepository.Register(user);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListUser()
        {
           var data = await _userRepository.ListUser();
            return Ok(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserDto userDto)
        {
            var user = new User()
            {
                User_Id = id,
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = userDto.Password
            };

            var updatedUser = await _userRepository.Update(user);

            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userToDelete = new User() { User_Id = id };

            var deletedUser = await _userRepository.Delete(userToDelete);

            if (deletedUser == null)
            {
                return NotFound();
            }

            return Ok(deletedUser);
        }
    }
}
