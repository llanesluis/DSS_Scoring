using DSS_Scoring.Data;
using DSS_Scoring.Models;
using Microsoft.AspNetCore.Mvc;

namespace DSS_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultadosController : ControllerBase
    {
        private MyDbContext _context;
        public ResultadosController(MyDbContext context)
        {
            _context = context;
        }

        // GET api/Resultados/
        [HttpGet]
        public IEnumerable<Resultado> GetAll()
        {
            var resultados = _context.Resultados.ToList();
            return resultados;

        }

        // GET: api/Resultados/{idProyecto}/{idAlternativa}
        [HttpGet("{idProyecto}/{idAlternativa}")]
        public async Task<ActionResult<Resultado>> GetById(int idProyecto, int idAlternativa)
        {
            var resultado = await _context.Resultados.FindAsync(idProyecto, idAlternativa);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        // POST: api/Resultados
        [HttpPost]
        public async Task<ActionResult<Resultado>> Post(Resultado resultado)
        {
            _context.Resultados.Add(resultado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { idProyecto = resultado.IdProyecto, idAlternativa = resultado.IdAlternativa }, resultado);
        }
    }

}
