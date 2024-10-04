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
    public class AlternativasController : ControllerBase
    {
        private MyDbContext _context;
        public AlternativasController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener una lista de todas las alternativas
        // GET: api/Alternativas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlternativaDTO>>> Get()
        {
            var alternativas = await _context.Alternativas.ToListAsync();

            var results = alternativas.Adapt<List<AlternativaDTO>>();

            return Ok(results);
        }

        // Obtener una alternativa por su Id y la Id del proyecto al que pertenece (ambas Id son necesarias)
        // GET: api/Alternativas/{id}/{idProyecto}
        [HttpGet("{id}/{idProyecto}")]
        public async Task<ActionResult<AlternativaDTO>> GetById(int id, int idProyecto)
        {
            var alternativa = await _context.Alternativas.FindAsync(id, idProyecto);

            if (alternativa == null)
            {
                return NotFound();
            }

            var result = alternativa.Adapt<AlternativaDTO>();

            return Ok(result);
        }

        // Obtener una lista de alternativas por el Id del proyecto al que pertenecen
        // GET: api/Alternativas/PorIdProyecto/{idProyecto}
        [HttpGet("PorIdProyecto/{idProyecto}")]
        public async Task<ActionResult<AlternativaDTO>> GetAlternativasPorIdProyecto(int idProyecto)
        {
            var proyecto = await _context.Proyectos.FindAsync(idProyecto);

            if (proyecto == null)
            {
                return NotFound();
            }

            var alternativasPorProyecto = await _context.Alternativas.Where(a => a.IdProyecto == idProyecto).ToListAsync();

            var results = alternativasPorProyecto.Adapt<List<AlternativaDTO>>();

            return Ok(results);
        }

        // Crear una alternativa, recibe un objeto con "IdProyecto" (debe ser un Id existente), "Nombre" y "Descripcion"
        // POST: api/Alternativas
        [HttpPost]
        public async Task<ActionResult<AlternativaDTO>> Post(AlternativaDTO _nuevaAlternativa)
        {
            var nuevaAlternativa = new Alternativa
            {
                IdProyecto = _nuevaAlternativa.IdProyecto,
                Nombre = _nuevaAlternativa.Nombre,
                Descripcion = _nuevaAlternativa.Descripcion
            };

            _context.Alternativas.Add(nuevaAlternativa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = nuevaAlternativa.Id, idProyecto = nuevaAlternativa.IdProyecto }, nuevaAlternativa.Adapt<AlternativaDTO>());
        }

        // Eliminar una alternativa por su Id y el Id del proyecto al que pertenece (ambas Id son necesarias y deben existir)
        // DELETE: api/Alternativas/Eliminar/{id}/{idProyecto}
        [HttpDelete("Eliminar/{id}/{idProyecto}")]
        public async Task<IActionResult> DeleteById(int id, int idProyecto)
        {
            var alternativa = await _context.Alternativas.FindAsync(id, idProyecto);

            if (alternativa == null)
            {
                return NotFound();
            }

            _context.Alternativas.Remove(alternativa);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
