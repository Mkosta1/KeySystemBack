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
public class SiteController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly SiteMapper _mapper;

    public SiteController(IAppUOW uow, IMapper automapper)
    {
        _uow = uow;
        _mapper = new SiteMapper(automapper);
    }

    // GET: api/Site
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Site>>> GetSite()
    {
        var data = await _uow.SiteRepository.AllAsync();
        var res = data
            .Select(e => _mapper.Map(e)!).ToList();
        return res;
    }

    // GET: api/Site/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Public.DTO.v1.Site>> GetSite(Guid id)
    {
        var job = await _uow.SiteRepository.FindAsync(id);

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
    public async Task<IActionResult> PutSite(Guid id, Public.DTO.v1.Site job)
    {
        if (id != job.Id)
        {
            return BadRequest();
        }
        

        var uowJob = _mapper.Map(job);
        _uow.SiteRepository.Update(uowJob!);

        return NoContent();
    }

    // POST: api/Jobs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Public.DTO.v1.Site>> PostSite(Public.DTO.v1.Site job)
    {
        var uowJob = _mapper.Map(job);
        _uow.SiteRepository.Add(uowJob!);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetSite", new { id = job.Id }, job);
    }

    // DELETE: api/Jobs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSite(Guid id)
    {
        var job = await _uow.SiteRepository.RemoveAsync(id, User.GetUserId());
        if (job == null) return NotFound();
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}
