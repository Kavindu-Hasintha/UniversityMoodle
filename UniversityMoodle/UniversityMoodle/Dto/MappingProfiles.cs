using AutoMapper;
using UniversityMoodle.Models;

namespace UniversityMoodle.Dto
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<UserSignupDto, User>();
            CreateMap<User, UserSignupDto>();
            CreateMap<User, UserDto>();
            CreateMap<Course, CourseDto>();
        }
    }
}
