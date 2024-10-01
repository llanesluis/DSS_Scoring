using DSS_Scoring.Data;
using DSS_Scoring.Models;
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

        // GET: api/Alternativas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alternativa>>> Get()
        {
            var alternativas = await _context.Alternativas.ToListAsync();
            return Ok(alternativas);
        }

        // GET: api/Alternativas/{id}/{idProyecto}
        [HttpGet("{id}/{idProyecto}")]
        public async Task<ActionResult<Alternativa>> GetById(int id, int idProyecto)
        {
            var alternativa = await _context.Alternativas.FindAsync(id, idProyecto);

            if (alternativa == null)
            {
                return NotFound();
            }

            return Ok(alternativa);
        }

        // POST: api/Alternativas
        [HttpPost]
        public async Task<ActionResult<Alternativa>> Post(Alternativa alternativa)
        {
            _context.Alternativas.Add(alternativa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = alternativa.Id, idProyecto = alternativa.IdProyecto }, alternativa);
        }
    }
}
