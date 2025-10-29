using Microsoft.AspNetCore.Mvc;
using personaapi_dotnet.Models.Entities;
using personaapi_dotnet.Repositories;

namespace personaapi_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionController : ControllerBase
    {
        private readonly IRepository<Profesion> _repo;

        public ProfesionController(IRepository<Profesion> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Profesion>> GetById(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Profesion>> Create(Profesion model)
        {
            if (string.IsNullOrWhiteSpace(model.Nom))
                return BadRequest("El campo 'nom' (nombre) es obligatorio.");

            await _repo.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Profesion model)
        {
            if (id != model.Id) return BadRequest("El id de la ruta y del cuerpo no coinciden.");
            var exists = await _repo.GetByIdAsync(id);
            if (exists == null) return NotFound();

            exists.Nom = model.Nom;
            exists.Des = model.Des;

            await _repo.UpdateAsync(exists);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exists = await _repo.GetByIdAsync(id);
            if (exists == null) return NotFound();

            await _repo.DeleteAsync(exists);
            return NoContent();
        }
    }
}
