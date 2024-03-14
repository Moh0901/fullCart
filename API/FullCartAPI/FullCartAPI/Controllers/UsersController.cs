using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FullCartAPI.Models;
using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;

namespace FullCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/Users
        [HttpGet]
        public IActionResult GetUsers()
        {
            var result = _userRepository.GetAllUsers();

            if (result == null && result.Count == 0)
            {
                return NotFound();
            }

            return Ok(result);
        }
        // GET: api/Users/5
          [HttpGet("{id}")]
          public IActionResult GetUserById(int id)
          {
            var user = _userRepository.GetUsersById(id);

            if (user == null)
            {
                return NotFound($"User Not Found of {id}.");
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddNewUser(UserViewModel userViewModel)

        {
            var newUser = _userRepository.AddNewUsers(userViewModel);

            if (userViewModel == null)
            {
                return NotFound("New User Not Added.");
            }

            return Ok(newUser);
        }

    }
}
