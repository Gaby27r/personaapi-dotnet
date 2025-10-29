using Microsoft.AspNetCore.Mvc;
using personaapi_dotnet.Models.Entities;
using personaapi_dotnet.Repositories;
using System.Threading.Tasks;

namespace personaapi_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaController : ControllerBase
    {
        private readonly IRepository<Persona> _repo;

        public PersonaController(IRepository<Persona> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _repo.GetAllAsync());

        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? NotFound() : Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Persona persona)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _repo.AddAsync(persona);
            return CreatedAtAction(nameof(GetById), new { id = persona.Cc }, persona);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> Update(long id, [FromBody] Persona persona)
        {
            if (id != persona.Cc) return BadRequest("El id de la URL no coincide con el de la entidad.");
            await _repo.UpdateAsync(persona);
            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _repo.DeleteAsync(entity);
            return NoContent();
        }

    }
}
