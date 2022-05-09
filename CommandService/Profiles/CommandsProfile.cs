using AutoMapper;
using CommandService.DTOs;
using CommandService.Models;
using CommandService.Protos;

namespace CommandService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // source -> target
            CreateMap<Platform, PlatformReadDTO>();
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<Command, CommandReadDTO>();
            CreateMap<PlatformPublishedDTO, Platform>()
                .ForMember(d => d.ExternalId, opt => opt.MapFrom(s => s.Id));
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(d => d.ExternalId, opt => opt.MapFrom(s => s.PlatformId));
        }
    }
}
