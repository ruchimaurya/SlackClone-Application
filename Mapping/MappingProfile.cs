using AutoMapper;
using SlackClone.Controllers.Resources;
using SlackClone.Models;
namespace SlackClone.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Users, UsersResource>();

            CreateMap<UsersResource, Users>()
                .ForMember(v => v.Id, opt => opt.Ignore());
        }
    }
}
