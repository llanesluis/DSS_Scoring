using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSS_Scoring.Data;
using DSS_Scoring.Models;
using Microsoft.EntityFrameworkCore;

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

        // GET api/Resultados
        [HttpGet]
        public IEnumerable<Resultado> Get()
        {
            var resultados = _context.Resultados.ToList();
            return resultados;

        }

        // GET: api/Resultados/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Resultado>> GetById(int id)
        {
            var resultado = await _context.Resultados.FindAsync();

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);

        }
    }

}
