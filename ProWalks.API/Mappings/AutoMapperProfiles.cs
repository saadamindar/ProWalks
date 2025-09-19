using AutoMapper;
using ProWalks.API.Models.Domain;
using ProWalks.API.Models.DTO;

namespace ProWalks.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionDto>().ReverseMap();
            CreateMap<Region, UpdateRegionDto>().ReverseMap(); 
            CreateMap<Walk, CreateWalkDto>().ReverseMap();
            CreateMap<Walk, WalkDto>().ReverseMap();
            CreateMap<Walk, UpdateWalkDto>().ReverseMap();
            CreateMap<Difficulty, DifficultyDto>().ReverseMap();
            
        }
    }
}
