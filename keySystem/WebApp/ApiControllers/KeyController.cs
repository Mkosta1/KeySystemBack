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
public class KeyController : ControllerBase
{
    private readonly IAppUOW _uow;
    private readonly KeyMapper _mapper;

    public KeyController(IAppUOW uow, IMapper automapper)
    {
        _uow = uow;
        _mapper = new KeyMapper(automapper);
    }

    // GET: api/Key
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Public.DTO.v1.Key>>> GetKey()
    {
        var data = await _uow.KeyRepository.AllAsync();
        var res = data
            .Select(e => _mapper.Map(e)!).ToList();
        return res;
    }

    // GET: api/Key/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Public.DTO.v1.Key>> GetKey(Guid id)
    {
        var job = await _uow.KeyRepository.FindAsync(id);

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
    public async Task<IActionResult> PutKey(Guid id, Public.DTO.v1.Key job)
    {
        if (id != job.Id)
        {
            return BadRequest();
        }
        

        var uowJob = _mapper.Map(job);
        _uow.KeyRepository.Update(uowJob!);

        return NoContent();
    }

    // POST: api/Jobs
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Public.DTO.v1.Key>> PostKey(Public.DTO.v1.Key job)
    {
        var uowJob = _mapper.Map(job);
        _uow.KeyRepository.Add(uowJob!);
        
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetKey", new { id = uowJob!.Id }, uowJob);
    }

    // DELETE: api/Jobs/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteKey(Guid id)
    {
        var job = await _uow.KeyRepository.RemoveAsync(id, User.GetUserId());
        if (job == null) return NotFound();
        await _uow.SaveChangesAsync();

        return NoContent();
    }
}
