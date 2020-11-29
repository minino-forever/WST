using AutoMapper;
using WST.Admin.Models;
using WST.Admin.Models.ViewModels.Breaking;

namespace WST.Admin.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Breaking, BreakingFormDto>()
                .ForMember(formDto => formDto.Id, cfg => cfg.MapFrom(b => b.Id))
                .ForMember(formDto => formDto.Description, cfg => cfg.MapFrom(b => b.Description))
                .ForMember(formDto => formDto.RepairMethod, cfg => cfg.MapFrom(b => b.RepairMethod))
                .ReverseMap();
        }        
    }
}