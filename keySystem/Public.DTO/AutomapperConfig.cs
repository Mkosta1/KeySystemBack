using AutoMapper;
namespace Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Domain.App.Site, Public.DTO.v1.Site>().ReverseMap();
        
        CreateMap<Domain.App.Key, Public.DTO.v1.Key>().ReverseMap();
        
        CreateMap<Domain.App.WorkerAtSite, Public.DTO.v1.WorkerAtSite>().ReverseMap();
        
        CreateMap<Domain.App.Worker, Public.DTO.v1.Worker>().ReverseMap();

        CreateMap<Domain.App.KeyAtSite, Public.DTO.v1.KeyAtSite>().ReverseMap();
        
    }
}