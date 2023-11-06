using AutoMapper;
using DAL.Base;
using Public.DTO.v1;
using Site = Domain.App.Site;

namespace Public.DTO.Mappers;

public class SiteMapper : BaseMapper<Site, Public.DTO.v1.Site>
{
    public SiteMapper(IMapper mapper) : base(mapper)
    {
    }
    

}