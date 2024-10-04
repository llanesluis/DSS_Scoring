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
    public class ResultadosController : ControllerBase
    {
        private MyDbContext _context;
        public ResultadosController(MyDbContext context)
        {
            _context = context;
        }

        // Obtener la lista de todos los resultados sin detalles
        // GET api/Resultados/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResultadoDTO>>> GetAll()
        {
            var res = await _context.Resultados.ToListAsync();

            var resultados = res.Adapt<List<ResultadoDTO>>();

            return Ok(resultados);

        }

        // Obtener la lista de todos los resultados con detalles
        // GET api/Resultados/Detalles
        [HttpGet("Detalles/")]
        public async Task<ActionResult<IEnumerable<ResultadoWithDetailsDTO>>> GetResultadosConDetalles()
        {
            var _resultados = await _context.Resultados.ToListAsync();

            List<ResultadoWithDetailsDTO> resultados = new List<ResultadoWithDetailsDTO>();

            foreach (var resultado in _resultados)
            {
                var proyecto = _context.Proyectos.Find(resultado.IdProyecto);
                var alternativa = _context.Alternativas.Find(resultado.IdAlternativa, resultado.IdProyecto);

                var _resultado = new ResultadoWithDetailsDTO
                {
                    IdProyecto = resultado.IdProyecto,
                    IdAlternativa = resultado.IdAlternativa,
                    Score = resultado.Score,
                    Proyecto = proyecto.Adapt<ProyectoDTO>(),
                    Alternativa = alternativa.Adapt<AlternativaDTO>()
                };

                resultados.Add(_resultado);
            }

            return Ok(resultados);

        }

        // Obtener la lista de todos los resultados con detalles para un proyecto específico
        // GET api/Resultados/PorIdProyecto/{idProyecto}
        [HttpGet("PorIdProyecto/{idProyecto}")]
        public async Task<ActionResult<IEnumerable<ResultadoWithDetailsDTO>>> GetResultadosPorProyectoIdConDetalles(int idProyecto)
        {
            var _resultados = await _context.Resultados.Where(r => r.IdProyecto == idProyecto).ToListAsync();

            List<ResultadoWithDetailsDTO> resultados = new List<ResultadoWithDetailsDTO>();

            foreach (var resultado in _resultados)
            {
                var proyecto = _context.Proyectos.Find(resultado.IdProyecto);
                var alternativa = _context.Alternativas.Find(resultado.IdAlternativa, resultado.IdProyecto);

                var _resultado = new ResultadoWithDetailsDTO
                {
                    IdProyecto = resultado.IdProyecto,
                    IdAlternativa = resultado.IdAlternativa,
                    Score = resultado.Score,
                    Proyecto = proyecto.Adapt<ProyectoDTO>(),
                    Alternativa = alternativa.Adapt<AlternativaDTO>()
                };

                resultados.Add(_resultado);
            }

            return Ok(resultados);

        }

        // Obtiene el registro de un resultado en específico, sin detalles.
        // Necesita el id del proyecto y el id de la alternativa (necesarios)
        // GET: api/Resultados/{idProyecto}/{idAlternativa}
        [HttpGet("{idProyecto}/{idAlternativa}")]
        public async Task<ActionResult<ResultadoDTO>> GetById(int idProyecto, int idAlternativa)
        {
            var res = await _context.Resultados.FindAsync(idProyecto, idAlternativa);

            if (res == null)
            {
                return NotFound();
            }

            var resultado = res.Adapt<ResultadoDTO>();

            return Ok(resultado);
        }


        // Obtener el registro de un resultado en específico, con detalles.
        // Necesita el id del proyecto y el id de la alternativa (necesarios)
        // GET api/Resultados/Detalles
        [HttpGet("Detalles/{idProyecto}/{idAlternativa}")]
        public async Task<ActionResult<ResultadoWithDetailsDTO>> GetResultadosConDetallesPorId(int idProyecto, int idAlternativa)
        {
            var res = await _context.Resultados.FindAsync(idProyecto, idAlternativa);

            if (res == null)
            {
                return NotFound();
            }

            var proyecto = _context.Proyectos.Find(idProyecto);
            var alternativa = _context.Alternativas.Find(idAlternativa, idProyecto);

            var resultado = new ResultadoWithDetailsDTO
            {
                IdProyecto = res.IdProyecto,
                IdAlternativa = res.IdAlternativa,
                Score = res.Score,
                Proyecto = proyecto.Adapt<ProyectoDTO>(),
                Alternativa = alternativa.Adapt<AlternativaDTO>()
            };

            return Ok(resultado);
        }

        // Crear un resultado, recibe un objeto con "IdProyecto" (debe ser un Id existente),
        // "IdAlternativa" (debe ser un Id existente) y "Score"
        // POST: api/Resultados
        [HttpPost]
        public async Task<ActionResult<ResultadoDTO>> Post(ResultadoDTO _resultado)
        {
            var resultado = new Resultado
            {
                IdProyecto = _resultado.IdProyecto,
                IdAlternativa = _resultado.IdAlternativa,
                Score = _resultado.Score
            };

            _context.Resultados.Add(resultado);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { idProyecto = resultado.IdProyecto, idAlternativa = resultado.IdAlternativa }, resultado.Adapt<ResultadoDTO>());
        }
    }

}
