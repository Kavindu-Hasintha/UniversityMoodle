using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityMoodle.Dto;
using UniversityMoodle.Models;
using UniversityMoodle.Services.Users;

namespace UniversityMoodle.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper; 
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("userDetails"), Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult GetUserDetails()
        {
            var email = _userService.GetEmail();
            
            var user = _mapper.Map<UserDto>(_userService.GetUser(email));

            var role = _userService.GetRole(user.Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(new { user, role.Name});
        }
    }
}
