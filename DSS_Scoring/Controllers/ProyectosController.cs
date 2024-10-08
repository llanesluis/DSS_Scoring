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
    public class ProyectosController : ControllerBase
    {
        private MyDbContext _context;
        public ProyectosController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener todos los proyectos
        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProyectoDTO>>> Get()
        {
            var proyectos = await _context.Proyectos.ToListAsync();

            var results = proyectos.Adapt<List<ProyectoDTO>>();

            return Ok(results);
        }

        // Obtener un proyecto por su id con todos los detalles (alternativas y criterios)
        // GET: api/Proyectos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ProyectoDTO>> GetById(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            var response = proyecto.Adapt<ProyectoDTO>();

            return Ok(response);
        }

        // Obtener un proyecto por su id con todos los detalles (alternativas y criterios)
        // GET: api/Proyectos/Detalles/{id}
        [HttpGet("Detalles/{id}")]
        public async Task<ActionResult<ProyectoWithDetailsDTO>> GetDetallesPorId(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            var alternativas = _context.Alternativas.ToList().Where(p => p.IdProyecto == id);
            var criterios = _context.Criterios.ToList().Where(c => c.IdProyecto == id); ;

            ProyectoWithDetailsDTO response = new ProyectoWithDetailsDTO
            {
                Id = proyecto.Id,
                Nombre = proyecto.Nombre,
                Objetivo = proyecto.Objetivo,
                Alternativas = alternativas.Adapt<List<AlternativaDTO>>(),
                Criterios = criterios.Adapt<List<CriterioDTO>>()
            };

            return Ok(response);
        }

        // Crear un nuevo proyecto, recibe un objeto con "Nombre" y "Objetivo"
        // POST: api/Proyectos
        [HttpPost]
        public async Task<ActionResult<ProyectoDTO>> Post(ProyectoDTO _nuevoProyecto)
        {
            var nuevoProyecto = new Proyecto
            {
                Nombre = _nuevoProyecto.Nombre,
                Objetivo = _nuevoProyecto.Objetivo
            };

            _context.Proyectos.Add(nuevoProyecto);
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

            return CreatedAtAction(nameof(GetById), new { id = nuevoProyecto.Id }, nuevoProyecto.Adapt<ProyectoDTO>()); // Devuelve 201 Created con la ruta del nuevo recurso
        }

        // Eliminar un proyecto por su id
        // POST: api/Proyectos
        [HttpDelete("Eliminar/{id}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return NoContent();

        }
    }
}
