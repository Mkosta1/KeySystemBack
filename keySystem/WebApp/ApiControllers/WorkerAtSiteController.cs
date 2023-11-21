using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Contracts.Base;
using DAL.Base;
using DAL.Contracts.App;
using DAL.DTO;
using Domain.App.Identity;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WorkerAtSiteController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly WorkerAtSiteMapper _mapper;
    private readonly UserManager<AppUser> _userManager;

    public WorkerAtSiteController(IAppUOW uow, IMapper automapper, UserManager<AppUser> userManager)
    {
        
        _uow = uow;
        _userManager = userManager;
        _mapper = new WorkerAtSiteMapper(automapper);
    }
    
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.WorkerAtSite>>> GetWorkerAtSite()
    {
        var data = await _uow.WorkerAtSiteRepository.AllAsync();
        var res = data
            .Where(o => o.Until == null)
            .Select(e => _mapper.Map(e)!).ToList();
        
        return res;
    }
    
    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.WorkerAtSite>>> GetWorkerAtSiteAll()
    {
        var data = await _uow.WorkerAtSiteRepository.AllAsync();
        var res = data
            .Where(o => o.Until != null)
            .Select(e => _mapper.Map(e)!).ToList();
        
        return res;
    }

    // GET: api/WorkerAtSite/5
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Public.DTO.v1.WorkerAtSite>> GetWorkerAtSite(Guid id)
    {
        var job = await _uow.WorkerAtSiteRepository.FindAsync(id);

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
    public async Task<IActionResult> PutWorkerAtSite(Guid id, Public.DTO.v1.WorkerAtSite job)
    {
        if (id != job.Id)
        {
            return BadRequest();
        }
        var uowJob = _mapper.Map(job);
        
        _uow.WorkerAtSiteRepository.Update(uowJob!);
        
        await _uow.SaveChangesAsync();
        
        return NoContent();
    }

    // POST: api/Jobs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Public.DTO.v1.WorkerAtSite>> PostWorkerAtSite(Public.DTO.v1.WorkerAtSite job)
    {
        var existingJob = await _uow.WorkerAtSiteRepository.AllAsync();
        
        foreach (var value in existingJob)
        {
            if (value.SiteId == job.SiteId && value.Until == null)
            {
                return Conflict("Already written out!");
            }
        }
        
        var uowJob = _mapper.Map(job);
        
        _uow.WorkerAtSiteRepository.Add(uowJob!);
        
        await _uow.SaveChangesAsync();
        
        return CreatedAtAction("GetWorkerAtSite", new { id = job.Id }, job);
    }

    // DELETE: api/Jobs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkerAtSite(Guid id)
    {
        var job = await _uow.WorkerAtSiteRepository.RemoveAsync(id, User.GetUserId());
        if (job == null) return NotFound();
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}
