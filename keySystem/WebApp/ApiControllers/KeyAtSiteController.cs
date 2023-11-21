using AutoMapper;
using DAL.Contracts.App;
using Domain.App;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Key = Public.DTO.v1.Key;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class KeyAtSiteController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly KeyAtSiteMapper _mapper;


    public KeyAtSiteController(IAppUOW uow, IMapper automapper)
    {
        _uow = uow;
        _mapper = new KeyAtSiteMapper(automapper);
    }

    // GET: api/KeyAtSiteAtSite
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.KeyAtSite>>> GetKeyAtSite()
    {
        var data = await _uow.KeyAtSiteRepository.AllAsync();
        var res = data
            .Select(e => _mapper.Map(e)!).ToList();
        return res;
    }

    // GET: api/KeyAtSite/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Public.DTO.v1.KeyAtSite>> GetKeyAtSite(Guid id)
    {
        var job = await _uow.KeyAtSiteRepository.FindAsync(id);
        
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
    public async Task<IActionResult> PutKeyAtSite(Guid id, Public.DTO.v1.KeyAtSite job)
    {
        if (id != job.Id)
        {
            return BadRequest();
        }
        

        var uowJob = _mapper.Map(job);
        _uow.KeyAtSiteRepository.Update(uowJob!);

        return NoContent();
    }

    // POST: api/Jobs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Public.DTO.v1.KeyAtSite>> PostKeyAtSite(Public.DTO.v1.KeyAtSite job)
    {
        var uowJob = _mapper.Map(job);
        _uow.KeyAtSiteRepository.Add(uowJob!);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetKeyAtSite", new { id = job.Id }, job);
    }

    // DELETE: api/Jobs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKeyAtSite(Guid id)
    {
        var job = await _uow.KeyAtSiteRepository.RemoveAsync(id, User.GetUserId());
        if (job == null) return NotFound();
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}
