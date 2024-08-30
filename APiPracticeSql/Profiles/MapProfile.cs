using ApiPractice.DAL.Entities;
using ApiPractice.DAL.Extensions;
using APiPracticeSql.Dtos.BookDtos;
using APiPracticeSql.Dtos.GroupDtos;
using APiPracticeSql.Dtos.StudentDtos;
using APiPracticeSql.Dtos.UserDtos;
using APiPracticeSql.Entities;
using AutoMapper;

namespace APiPracticeSql.Profiles
{
    public class MapProfile:Profile
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public MapProfile(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;

            var uriBuilder = new UriBuilder
                (
                _contextAccessor.HttpContext.Request.Scheme,
                _contextAccessor.HttpContext.Request.Host.Host,
               (int) _contextAccessor.HttpContext.Request.Host.Port
                );
            var url = uriBuilder.Uri.AbsoluteUri;

            //student
            CreateMap<StudentCreateDto, Student>();
            CreateMap<Student, StudentReturnDto>();
            CreateMap<Student, StudentInGroupReturnDto>();
            CreateMap<StudentUpdateDto, Student>();

            CreateMap<Group,GroupInStudentReturnDto>();
            


            //group
            CreateMap<Group, GroupReturnDto>()
                .ForMember(d => d.Image, map => map.MapFrom(s => url+"images/"+s.Image));

            CreateMap<GroupCreateDto, Group>()
                .ForMember(d => d.Image, map => map.MapFrom(d => d.File.Save(Directory.GetCurrentDirectory(), "images")));


            //book
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookAuthor, AuthorInBookReturnDto>();
            CreateMap<Book, BookReturnDto>();
            CreateMap<BookUpdateDto, Book>();

            //user
            CreateMap<AppUser, UserReturnDto>();
        }
    }
}
