using AutoMapper;
using DAL.Base;
using Domain.App;

namespace Public.DTO.Mappers;

public class KeyMapper : BaseMapper<Key, Public.DTO.v1.Key>
{
    public KeyMapper(IMapper mapper) : base(mapper)
    {
    }
    

}