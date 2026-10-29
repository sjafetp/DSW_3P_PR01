using Microsoft.AspNetCore.Mvc;
using DSW_3P_PR01.Models;
using DSW_3P_PR01.Services;

namespace DSW_3P_PR01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MisionesController : ControllerBase
{
    private readonly IMisionService _service;

    public MisionesController(IMisionService service)
    {
        _service = service;
    }

    //GET - Obtener todas las misiones
    [HttpGet]
    [ProducesResponseType(typeof(List<Mision>),200)]
    public IActionResult ObtenerTodas()
    {
        return Ok(_service.ObtenerTodas());
    }

    //GET - Obtener mision por Id
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Mision),200)]
    [ProducesResponseType(404)]
    public IActionResult ObtenerPorId(int id)
    {
        var mision = _service.ObtenerPorId(id);
        if(mision == null)
            return NotFound();

        return Ok(mision);
    }

    //POST - Agregar nueva mision
    [HttpPost]
    [ProducesResponseType(typeof(Mision),200)]
    [ProducesResponseType(404)]
    public IActionResult Agregar(Mision nueva)
    {
        try
        {
            var creado = _service.Agregar(nueva);

            return CreatedAtAction(nameof(ObtenerPorId), new{id = creado.Id}, creado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new{mensaje=ex.Message});
        }

    }

    //PUT - Actualizar una mision
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)] //No content
    [ProducesResponseType(404)] //Not found
    [ProducesResponseType(400)] //Bad request
    public IActionResult Actualizar(int id, Mision actualizada)
    {
        try
        {
            bool resultado = _service.Actualizar(id, actualizada);
            if(!resultado)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new{mensaje=ex.Message});
        }
    }

    //DELETE - Eliminar una mision
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult Eliminar(int id)
    {
        bool resultado = _service.Eliminar(id);
        if(!resultado)
            return NotFound();

        return NoContent();
    }
}