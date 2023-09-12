using JwtAuthMicroservice.Models;
using JwtAuthMicroservice.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace JwtAuthMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly JwtSettings _jwtSettings;
        private readonly UserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(IConfiguration config, IOptions<JwtSettings> jwtSettings, UserRepository userRepository, JwtService jwtService)
        {
            _config = config;
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var user = _userRepository.GetByUsername(model.Username);

            if (user == null)
            {
                return Unauthorized();
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
            var hashed = BCrypt.Net.BCrypt.HashPassword(model.Password);
            if (!isPasswordValid)
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerateToken(model.Username);
            return Ok(new { token });
            
        }

        [HttpPost("registration")]
        public IActionResult Registration([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingUser = _userRepository.GetByUsername(registerModel.Username);
            if (existingUser != null)
            {
                return BadRequest("Username is already taken.");
            }
            // hash password before creating user
            registerModel.Password = BCrypt.Net.BCrypt.HashPassword(registerModel.Password);
            User newUser = new User
            {
                Username = registerModel.Username,
                Email = registerModel.Email,
                Password = registerModel.Password // Don't forget to hash the password!
            };
            _userRepository.Create(newUser);
            var token = _jwtService.GenerateToken(registerModel.Username);
            return Ok(new {token});
        }

    }

}


// TODO Unit TESTing

