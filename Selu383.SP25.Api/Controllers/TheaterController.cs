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
            .Select(t => new TheaterDto
            {
                Id = t.Id,
                Name = t.Name,
                Address = t.Address,
                SeatCount = t.SeatCount
            })
            .ToListAsync();

        return Ok(theaters);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TheaterDto>> GetById(int id)
    {
        var theater = await _context.Theaters.FindAsync(id);
        if (theater == null) return NotFound();
        return Ok(new TheaterDto { Id = theater.Id, Name = theater.Name, Address = theater.Address, SeatCount = theater.SeatCount });
    }

    [HttpPost]
    public async Task<ActionResult<TheaterDto>> Create([FromBody] TheaterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var theater = new Theater
        {
            Name = dto.Name,
            Address = dto.Address,
            SeatCount = dto.SeatCount
        };

        _context.Theaters.Add(theater);
        await _context.SaveChangesAsync();

        dto.Id = theater.Id;
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
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
        return Ok(dto);
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
