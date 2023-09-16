using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using UniversityMoodle.Dto;
using UniversityMoodle.Models;
using UniversityMoodle.Services.Roles;
using UniversityMoodle.Services.Users;

namespace UniversityMoodle.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        public AuthController(IConfiguration configuration, IUserService userService, IMapper mapper, IRoleService roleService)
        {
            _configuration = configuration;
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
        }

        [HttpPost("signup")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Signup([FromBody] UserSignupDto userSignup)
        {
            if (userSignup == null)
            {
                return BadRequest(ModelState);
            }

            if (userSignup.Name.Length == 0 || userSignup.Email.Length == 0 || userSignup.DoB == null
                || userSignup.Password.Length == 0 || userSignup.Role == null) 
            {
                ModelState.AddModelError("SignupError", "Please fill all the fields");
                return StatusCode(422, ModelState);
            }

            if (!EmailValidation(userSignup.Email))
            {
                ModelState.AddModelError("SignupError", "Invalid email address");
                return StatusCode(422, ModelState);
            }

            if (!userSignup.Password.Equals(userSignup.ConfirmPassword))
            {
                ModelState.AddModelError("SignupError", "Confirm password is not matching");
                return StatusCode(422, ModelState);
            }

            if (_userService.UserExist(userSignup.Email))
            {
                ModelState.AddModelError("SignupError", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreatePasswordHash(userSignup.Password, out byte[] passwordHash, out byte[] passwordSalt);

            // var userMap = _mapper.Map<User>(userSignup);
            var userMap = new User();
            userMap.Name = userSignup.Name;
            userMap.Email = userSignup.Email;
            userMap.PasswordHash = passwordHash;
            userMap.PasswordSalt = passwordSalt;
            userMap.DoB = userSignup.DoB;
            userMap.Role = _roleService.GetRole(userSignup.Role);

            if (!_userService.CreateUser(userMap))
            {
                ModelState.AddModelError("SignupError", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Registration Success");
        }

        [HttpPost("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Login([FromBody] UserLoginDto userLogin)
        {
            if (userLogin == null)
            {
                return BadRequest();
            }

            if (!EmailValidation(userLogin.Email))
            {
                ModelState.AddModelError("LoginError", "Invalid email address");
                return StatusCode(422, ModelState);
            }

            var user = _userService.GetUsers().Where(u => u.Email.Trim().ToUpper() == userLogin.Email.TrimEnd().ToUpper()).FirstOrDefault();

            if (user == null)
            {
                ModelState.AddModelError("LoginError", "Login failed");
                return StatusCode(422, ModelState);
            }

            if (!VerifyPasswordHash(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Login failed");
            }

            Role role = _userService.GetRole(user.Id);

            string token = CreateToken(user, role.Name);

            return Ok(new { token, role.Name });
        }

        private bool EmailValidation(string email)
        {
            var emailValidation = new EmailAddressAttribute();
            return emailValidation.IsValid(email);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user, string role)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
