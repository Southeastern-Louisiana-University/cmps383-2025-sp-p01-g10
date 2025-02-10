using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Data;
using Selu383.SP25.Api.Dtos;
using Selu383.SP25.Api.Entities;

[Route("api/theaters")]
[ApiController]
public class TheatersController : ControllerBase
{
    private readonly DataContext _context;

    public TheatersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<TheaterDto>>> GetAll()
    {
        var theaters = await _context.Theaters
            .ToListAsync();

        return Ok(theaters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TheaterDto>> GetById(int id)
    {
        var theater = await _context.Theaters.FindAsync(id);
        if (theater == null) return NotFound();
        return Ok(theater);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Theater theater)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _context.Theaters.AddAsync(theater);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = theater.Id }, theater);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TheaterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var theater = await _context.Theaters.FindAsync(id);
        if (theater == null) return NotFound();

        theater.Name = dto.Name;
        theater.Address = dto.Address;
        theater.SeatCount = dto.SeatCount;

        await _context.SaveChangesAsync();
        return Ok(theater);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var theater = await _context.Theaters.FindAsync(id);
        if (theater == null) return NotFound();

        _context.Theaters.Remove(theater);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
