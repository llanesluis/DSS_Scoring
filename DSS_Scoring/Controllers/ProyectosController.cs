using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSS_Scoring.Data;
using DSS_Scoring.Models;
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
            var projects = await _context.Proyectos.ToListAsync();

            return projects; // Devuelve 200 OK con los proyectos
        }

        // GET: api/Proyectos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetById(int id)
        {
            var project = await _context.Proyectos.FindAsync(id);

            if (project == null)
            {
                return NotFound(); // Devuelve 404 si el proyecto no existe
            }

            return Ok(project); // Devuelve 200 OK con el proyecto
        }

        // POST: api/Proyectos
        [HttpPost]
        public async Task<ActionResult<Proyecto>> Post(Proyecto newProject)
        {
            _context.Proyectos.Add(newProject);
            await _context.SaveChangesAsync(); // Guarda los cambios en la base de datos

            return CreatedAtAction(nameof(GetById), new { id = newProject.Id }, newProject); // Devuelve 201 Created con la ruta del nuevo recurso
        }
    }
}
