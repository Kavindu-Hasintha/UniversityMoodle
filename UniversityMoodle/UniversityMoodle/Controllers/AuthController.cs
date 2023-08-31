using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using UniversityMoodle.Dto;
using UniversityMoodle.Models;
using UniversityMoodle.Services.User;

namespace UniversityMoodle.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userRepository;
        public AuthController(IConfiguration configuration, IUserService userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
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

            if (_userRepository.UserExist(userSignup.Email))
            {
                ModelState.AddModelError("SignupError", "User already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }



            return Ok();
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
