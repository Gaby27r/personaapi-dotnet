using Microsoft.AspNetCore.Mvc;
using personaapi_dotnet.Models.Entities;
using personaapi_dotnet.Repositories;

namespace personaapi_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstudioController : ControllerBase
{
    private readonly IRepository<Estudio> _repo;

    public EstudioController(IRepository<Estudio> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Estudio>>> GetAll()
    {
        var list = await _repo.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{idProf:int}/{ccPer:int}", Name = "GetEstudioByIds")]
    public async Task<ActionResult<Estudio>> GetByIds(int idProf, int ccPer)
    {
        var entity = await _repo.GetByIdsAsync(idProf, ccPer);
        if (entity == null) return NotFound();
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<Estudio>> Create(Estudio model)
    {
        if (model.IdProf <= 0 || model.CcPer <= 0)
            return BadRequest("Debe indicar un IdProf y un CcPer válidos.");

        await _repo.AddAsync(model);

        return CreatedAtRoute("GetEstudioByIds",
            new { idProf = model.IdProf, ccPer = model.CcPer }, model);
    }

    [HttpPut("{idProf:int}/{ccPer:int}")]
    public async Task<IActionResult> Update(int idProf, int ccPer, Estudio model)
    {
        if (idProf != model.IdProf || ccPer != model.CcPer)
            return BadRequest("Las llaves de ruta y cuerpo no coinciden.");

        var exists = await _repo.GetByIdsAsync(idProf, ccPer);
        if (exists == null) return NotFound();

        exists.Fecha = model.Fecha;
        exists.Univer = model.Univer;

        await _repo.UpdateAsync(exists);
        return NoContent();
    }

    [HttpDelete("{idProf:int}/{ccPer:int}")]
    public async Task<IActionResult> Delete(int idProf, int ccPer)
    {
        var entity = await _repo.GetByIdsAsync(idProf, ccPer);
        if (entity == null) return NotFound();

        await _repo.DeleteAsync(entity);
        return NoContent();
    }
}
