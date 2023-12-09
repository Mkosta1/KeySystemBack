using System.Text.Json;
using System.Text.Json.Serialization;
using System.Transactions;
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
using Microsoft.EntityFrameworkCore;
using Public.DTO.Mappers;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class WorkerAtSiteController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly WorkerAtSiteMapper _mapper;

    public WorkerAtSiteController(IAppUOW uow, IMapper automapper)
    {
        
        _uow = uow;
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
        
        var uow = _mapper.Map(job);
        
        var keyAtSite = await _uow.KeyAtSiteRepository.AllAsync(User.GetUserId());

        foreach (var item in keyAtSite)
        {
            if (item.SiteId == uow!.SiteId)
            {
                var key = await _uow.KeyRepository.FindAsync(item.KeyId);
                key!.Copies++;
            }
        }
        
        _uow.WorkerAtSiteRepository.Update(uow!);
        
        await _uow.SaveChangesAsync();
        
        return NoContent();
    }

    
[HttpPost]
public async Task<ActionResult<Public.DTO.v1.WorkerAtSite>> PostWorkerAtSite(Public.DTO.v1.WorkerAtSite job)
{
    var existingJob = await _uow.WorkerAtSiteRepository.AllAsync();

    var uow = _mapper.Map(job);

    // Gets all keys and sites from KeyAtSite table
    var keyAtSite = await _uow.KeyAtSiteRepository.AllAsync(User.GetUserId());
    
    try
    {
        foreach (var item in keyAtSite)
        {
            // Checks where the current post siteId matches with KeyAtSite binding table
            if (item.SiteId == uow!.SiteId)
            {
                // Checks if there are enough copies for the current key
                var key = await _uow.KeyRepository.FindAsync(item.KeyId);
                if (key?.Copies != 0)
                {
                    key!.Copies--;
                }
                else
                {
                    return BadRequest("Failed to add worker at site.");
                    
                }
            }
        }

        // Apply changes to the database
        var uowJob = _mapper.Map(job);
        _uow.WorkerAtSiteRepository.Add(uowJob!);

        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetWorkerAtSite", new { id = job.Id }, job);
    }
    catch (DbUpdateException ex) when (ex.InnerException is Npgsql.PostgresException pgEx && pgEx.SqlState == "40001")
    {
        // Retry the transaction in case of a transient failure
        return await PostWorkerAtSite(job);
    }
    catch (Exception)
    {
        // The transaction will be rolled back automatically if an exception occurs
        return BadRequest("Failed to add worker at site.");
    }
    
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
