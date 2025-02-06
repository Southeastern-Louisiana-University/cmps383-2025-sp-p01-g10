using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Entities;
using Selu383.SP25.Api.Dtos;

namespace Selu383.SP25.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TheatersController : ControllerBase
    {
        public class TheaterDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public int SeatCount { get; set; }
        }

        private static List<TheaterDto> theaters = new List<TheaterDto>
        {
            new TheaterDto { Id = 1, Name = "Grand Cinema", Address = "Downtown", SeatCount = 150 },
            new TheaterDto { Id = 2, Name = "Movie Palace", Address = "Uptown", SeatCount = 200 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<TheaterDto>> GetAllTheaters()
        {
            return Ok(theaters);
        }

        [HttpGet("{id}")]
        public ActionResult<TheaterDto> GetTheaterById(int id)
        {
            var theater = theaters.FirstOrDefault(t => t.Id == id);
            if (theater == null) return NotFound("Theater not found.");
            return Ok(theater);
        }

        [HttpPost]
        public ActionResult<TheaterDto> CreateTheater([FromBody] TheaterDto theaterDto)
        {
            if (string.IsNullOrEmpty(theaterDto.Name) || theaterDto.Name.Length > 120 ||
                string.IsNullOrEmpty(theaterDto.Address) || theaterDto.SeatCount < 1)
            {
                return BadRequest("Invalid input. Ensure name, address, and seat count are valid.");
            }

            theaterDto.Id = theaters.Count + 1;
            theaters.Add(theaterDto);
            return CreatedAtAction(nameof(GetTheaterById), new { id = theaterDto.Id }, theaterDto);
        }

        [HttpPut("{id}")]
        public ActionResult<TheaterDto> UpdateTheater(int id, [FromBody] TheaterDto theaterDto)
        {
            var theater = theaters.FirstOrDefault(t => t.Id == id);
            if (theater == null) return NotFound("Theater not found.");

            if (string.IsNullOrEmpty(theaterDto.Name) || theaterDto.Name.Length > 120 ||
                string.IsNullOrEmpty(theaterDto.Address) || theaterDto.SeatCount < 1)
            {
                return BadRequest("Invalid input. Ensure name, address, and seat count are valid.");
            }

            theater.Name = theaterDto.Name;
            theater.Address = theaterDto.Address;
            theater.SeatCount = theaterDto.SeatCount;
            return Ok(theater);
        }

        [HttpDelete("{id}")]
        public ActionResult<TheaterDto> DeleteTheater(int id)
        {
            var theater = theaters.FirstOrDefault(t => t.Id == id);
            if (theater == null) return NotFound("Theater not found.");

            theaters.Remove(theater);
            return Ok(theater);
        }
    }
}

