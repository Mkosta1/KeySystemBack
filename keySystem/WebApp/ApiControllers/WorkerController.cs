using AutoMapper;
using Contracts.Base;
using DAL.Contracts.App;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WorkerController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly WorkerMapper _mapper;

    public WorkerController(IAppUOW uow, IMapper automapper)
    {
        _uow = uow;
        _mapper = new WorkerMapper(automapper);
    }

    // GET: api/Worker
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Worker>>> GetWorker()
    {
        var data = await _uow.WorkerRepository.AllAsync();
        var res = data
            .Select(e => _mapper.Map(e)!).ToList();
        return res;
    }

    // GET: api/Worker/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Public.DTO.v1.Worker>> GetWorker(Guid id)
    {
        var job = await _uow.WorkerRepository.FindAsync(id);

        if (job == null)
        {
            return NotFound();
        }

        var res = _mapper.Map(job)!;

        return res;
    }

    // PUT: api/Jobs/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutWorker(Guid id, Public.DTO.v1.Worker job)
    {
        if (id != job.Id)
        {
            return BadRequest();
        }
        

        var uowJob = _mapper.Map(job);
        _uow.WorkerRepository.Update(uowJob!);

        return NoContent();
    }

    // POST: api/Jobs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Public.DTO.v1.Worker>> PostWorker(Public.DTO.v1.Worker job)
    {
        var uowJob = _mapper.Map(job);
        _uow.WorkerRepository.Add(uowJob!);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetWorker", new { id = job.Id }, job);
    }

    // DELETE: api/Jobs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorker(Guid id)
    {
        var job = await _uow.WorkerRepository.RemoveAsync(id, User.GetUserId());
        if (job == null) return NotFound();
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}
