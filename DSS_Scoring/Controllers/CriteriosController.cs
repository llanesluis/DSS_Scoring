using DSS_Scoring.Data;
using DSS_Scoring.Models;
using DSS_Scoring.Shared.DTOs;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSS_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriteriosController : ControllerBase
    {
        private MyDbContext _context;
        public CriteriosController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener una lista de todas los criterios
        // GET: api/Criterios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CriterioDTO>>> Get()
        {
            var criterios = await _context.Criterios.ToListAsync();

            var results = criterios.Adapt<List<CriterioDTO>>();

            return Ok(results);
        }

        // Obtener el criterio individual por su Id y la Id del proyecto al que pertenece (ambas Id son necesarias)
        // GET: api/Criterios/{id}/{idProyecto}
        [HttpGet("{id}/{idProyecto}")]
        public async Task<ActionResult<Criterio>> GetById(int id, int idProyecto)
        {
            var criterio = await _context.Criterios.FindAsync(id, idProyecto);

            if (criterio == null)
            {
                return NotFound();
            }

            var result = criterio.Adapt<CriterioDTO>();

            return Ok(result);
        }

        // Obtener una lista de criterios por el Id del proyecto al que pertenecen
        // GET: api/Criterios/PorIdProyecto/{idProyecto}
        [HttpGet("PorIdProyecto/{idProyecto}")]
        public async Task<ActionResult<CriterioDTO>> GetCriteriosPorIdProyecto(int idProyecto)
        {
            var proyecto = await _context.Proyectos.FindAsync(idProyecto);

            if (proyecto == null)
            {
                return NotFound();
            }

            var criteriosPorProyecto = await _context.Criterios.Where(c => c.IdProyecto == idProyecto).ToListAsync();

            var results = criteriosPorProyecto.Adapt<List<CriterioDTO>>();

            return Ok(results);
        }

        // Crear un nuevo criterio, recibe un objeto con "IdProyecto" (debe ser un Id existente), "Nombre", "Descripcion" y "Peso" (1-10)
        // POST: api/Criterios
        [HttpPost]
        public async Task<ActionResult<CriterioDTO>> Post(CriterioDTO _nuevoCriterio)
        {
            var nuevoCriterio = new Criterio
            {
                IdProyecto = _nuevoCriterio.IdProyecto,
                Nombre = _nuevoCriterio.Nombre,
                Descripcion = _nuevoCriterio.Descripcion,
                Peso = _nuevoCriterio.Peso
            };

            _context.Criterios.Add(nuevoCriterio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = nuevoCriterio.Id, idProyecto = nuevoCriterio.IdProyecto }, nuevoCriterio.Adapt<CriterioDTO>());

        }

        // Eliminar un criterio por su Id y el Id del proyecto al que pertenece (ambas Id son necesarias y deben existir)
        // DELETE: api/Criterios/Eliminar/{id}/{idProyecto}
        [HttpDelete("Eliminar/{id}/{idProyecto}")]
        public async Task<IActionResult> DeleteById(int id, int idProyecto)
        {
            var criterio = await _context.Criterios.FindAsync(id, idProyecto);

            if (criterio == null)
            {
                return NotFound();
            }

            _context.Criterios.Remove(criterio);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
