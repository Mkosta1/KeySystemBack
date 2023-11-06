using AutoMapper;
using DAL.Base;
using Domain.App;
using WorkerAtSite = Domain.App.WorkerAtSite;

namespace Public.DTO.Mappers;

public class WorkerMapper : BaseMapper<Worker, Public.DTO.v1.Worker>
{
    public WorkerMapper(IMapper mapper) : base(mapper)
    {
    }
    

}