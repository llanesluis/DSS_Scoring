using DSS_Scoring.Data;
using DSS_Scoring.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatricesController : ControllerBase
    {
        private MyDbContext _context;
        public MatricesController(MyDbContext context)
        {
            _context = context;
        }

        // GET api/Matrices
        [HttpGet]
        public IEnumerable<Matriz> Get()
        {
            var matrices = _context.Matrices.ToList();
            return matrices;

        }

        // GET: api/Matrices/{idProyecto}/{idAlternativa}/{idCriterio}
        [HttpGet("{idProyecto}/{idAlternativa}/{idCriterio}")]
        public async Task<ActionResult<Matriz>> GetById(int idProyecto, int idAlternativa, int idCriterio)
        {
            var matriz = await _context.Matrices.FindAsync(idProyecto, idAlternativa, idCriterio);

            if (matriz == null)
            {
                return NotFound();
            }

            return Ok(matriz);
        }

        // POST: api/Matrices
        [HttpPost]
        public async Task<ActionResult<Matriz>> Post(Matriz matriz)
        {
            _context.Matrices.Add(matriz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { idProyecto = matriz.IdProyecto, idAlternativa = matriz.IdAlternativa, idCriterio = matriz.IdCriterio }, matriz);
        }
    }
}
