using AutoMapper;
using DAL.Base;
using WorkerAtSite = Domain.App.WorkerAtSite;

namespace Public.DTO.Mappers;

public class WorkerAtSiteMapper : BaseMapper<WorkerAtSite, Public.DTO.v1.WorkerAtSite>
{
    public WorkerAtSiteMapper(IMapper mapper) : base(mapper)
    {
    }
    

}