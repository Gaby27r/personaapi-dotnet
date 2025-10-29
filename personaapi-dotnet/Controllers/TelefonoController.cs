using Microsoft.AspNetCore.Mvc;
using personaapi_dotnet.Models.Entities;
using personaapi_dotnet.Repositories;

namespace personaapi_dotnet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelefonoController : ControllerBase
    {
        private readonly IRepository<Telefono> _repo;

        public TelefonoController(IRepository<Telefono> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetAll()
        {
            var list = await _repo.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{num}")]
        public async Task<ActionResult<Telefono>> GetById(string num)
        {
            var entity = await _repo.GetByIdAsync(num);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<Telefono>> Create(Telefono model)
        {
            if (string.IsNullOrWhiteSpace(model.Num))
                return BadRequest("El campo 'num' es obligatorio.");

            await _repo.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { num = model.Num }, model);
        }

        [HttpPut("{num}")]
        public async Task<IActionResult> Update(string num, Telefono model)
        {
            if (!string.Equals(num, model.Num, StringComparison.Ordinal))
                return BadRequest("El número de la ruta y del cuerpo no coinciden.");

            var exists = await _repo.GetByIdAsync(num);
            if (exists == null) return NotFound();

            exists.Oper = model.Oper;
            exists.Duenio = model.Duenio;

            await _repo.UpdateAsync(exists);
            return NoContent();
        }

        [HttpDelete("{num}")]
        public async Task<IActionResult> Delete(string num)
        {
            var exists = await _repo.GetByIdAsync(num);
            if (exists == null) return NotFound();

            await _repo.DeleteAsync(exists);
            return NoContent();
        }
    }
}
