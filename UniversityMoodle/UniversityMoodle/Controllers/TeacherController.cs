using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityMoodle.Dto;
using UniversityMoodle.Models;
using UniversityMoodle.Services.Users;

namespace UniversityMoodle.Controllers
{
    [Route("teacher")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public TeacherController(IUserService userService, IMapper mapper) 
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("getcourses"), Authorize(Roles = "Teacher")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetCoursesByTeacher()
        {
            var email = _userService.GetEmail();

            var user = _mapper.Map<UserDto>(_userService.GetUser(email));

            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var courses = _mapper.Map<List<CourseDto>>(_userService.GetCoursesByTeacher(user.Id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(courses);
        }

        [HttpGet("getstudents"), Authorize(Roles = "Teacher")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetStudentsByTeacher()
        {
            var email = _userService.GetEmail();

            var user = _mapper.Map<UserDto>(_userService.GetUser(email));

            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var students = _mapper.Map<List<UserDto>>(_userService.GetStudentsByTeacher(user.Id));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(students);
        }
    }
}
