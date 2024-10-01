using DSS_Scoring.Client.Pages;
using DSS_Scoring.Data;
using DSS_Scoring.Models;
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

        // GET: api/Criterios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Criterio>>> Get()
        {
            var criterios = await _context.Criterios.ToListAsync();
            return Ok(criterios);
        }

        // GET: api/Criterios/{id}/{idProyecto}
        [HttpGet("{id}/{idProyecto}")]
        public async Task<ActionResult<Criterio>> GetById(int id, int idProyecto)
        {
            var criterio = await _context.Criterios.FindAsync(id, idProyecto);

            if (criterio == null)
            {
                return NotFound();
            }

            return Ok(criterio);
        }

        // POST: api/Criterios
        [HttpPost]
        public async Task<ActionResult<Criterio>> Post(Criterio criterio)
        {
            _context.Criterios.Add(criterio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = criterio.Id, idProyecto = criterio.IdProyecto }, criterio);

        }
    }
}
