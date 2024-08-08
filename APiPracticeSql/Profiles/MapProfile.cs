using APiPracticeSql.Dtos.GroupDtos;
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
            CreateMap<Student, StudentInGroupReturnDto>();
            CreateMap<Group, GroupReturnDto>()
                .ForMember(d => d.Image, map => map.MapFrom(s => url+"images/"+s.Image));

        }
    }
}
