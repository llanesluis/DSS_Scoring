using DSS_Scoring.Data;
using DSS_Scoring.Models;
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

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proyecto>>> Get()
        {
            var proyectos = await _context.Proyectos.ToListAsync();

            return proyectos;
        }

        // GET: api/Proyectos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetById(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return Ok(proyecto);
        }

        // POST: api/Proyectos
        [HttpPost]
        public async Task<ActionResult<Proyecto>> Post(Proyecto nuevoProyecto)
        {
            _context.Proyectos.Add(nuevoProyecto);
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

            return CreatedAtAction(nameof(GetById), new { id = nuevoProyecto.Id }, nuevoProyecto); // Devuelve 201 Created con la ruta del nuevo recurso
        }
    }
}
