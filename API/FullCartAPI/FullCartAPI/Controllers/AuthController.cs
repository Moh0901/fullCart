using FullCartAPI.Repository.Interfaces;
using FullCartAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenHandler _tokenHandler;
        private readonly ILoginRepository _loginRepository;

        public AuthController(ITokenHandler tokenHandler, ILoginRepository loginRepository)
        {
            _tokenHandler = tokenHandler;
            _loginRepository = loginRepository;
        }

        [HttpPost]

        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var result = _loginRepository.Authenticate(loginViewModel);

            if (result == null)
            {
                return BadRequest("Username or Password is incorrect.");
            }

            var token = _tokenHandler.CreateToken(result);
            return Ok(token);
        }
    }
}
