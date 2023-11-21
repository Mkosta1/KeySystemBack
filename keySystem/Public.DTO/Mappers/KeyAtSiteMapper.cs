using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class KeyAtSiteMapper : BaseMapper<KeyAtSite, Public.DTO.v1.KeyAtSite>
{
    public KeyAtSiteMapper(IMapper mapper) : base(mapper)
    {
        
    }
    

}