using DSS_Scoring.Data;
using DSS_Scoring.DTOs;
using DSS_Scoring.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Obtener todos los registros, sin detalles y sin agrupar
        // GET api/Matrices
        [HttpGet]
        public IEnumerable<MatrizDTO> Get()
        {
            var matrices = _context.Matrices.ToList();

            var results = matrices.Adapt<List<MatrizDTO>>();

            return results;
        }

        // Obtener un solo campo de la matriz por el IdProyecto, IdAlternativa e IdCriterio al que pertenece (los tres Id son necesarios)
        // GET: api/Matrices/{idProyecto}/{idAlternativa}/{idCriterio}
        [HttpGet("{idProyecto}/{idAlternativa}/{idCriterio}")]
        public async Task<ActionResult<MatrizDTO>> GetById(int idProyecto, int idAlternativa, int idCriterio)
        {
            var matriz = await _context.Matrices.FindAsync(idProyecto, idAlternativa, idCriterio);

            if (matriz == null)
            {
                return NotFound();
            }

            var response = matriz.Adapt<MatrizDTO>();

            return Ok(response);
        }


        // Obtener los registros de la matriz de un proyecto, agrupados por alternativa. Con todos los detalles
        // GET: api/Matrices/PorIdProyecto/{idProyecto}
        [HttpGet("PorIdProyecto/{idProyecto}")]
        public async Task<ActionResult<List<AlternativaWithDetailsDTO>>> GetMatrizPorIdProyecto(int idProyecto)
        {
            var proyecto = await _context.Proyectos.FindAsync(idProyecto);
            if (proyecto == null)
            {
                return NotFound();
            }

            // Obtener los datos crudos por el id del proyecto
            var matrices = await _context.Matrices.Where(m => m.IdProyecto == idProyecto).ToListAsync();
            var alternativas = _context.Alternativas.ToList().Where(p => p.IdProyecto == idProyecto);
            var criterios = _context.Criterios.ToList().Where(c => c.IdProyecto == idProyecto);


            List<AlternativaWithDetailsDTO> resultados = new List<AlternativaWithDetailsDTO>();

            // Agrupar las matrices por alternativa, se crearán tantos grupos como alternativas haya
            // Cada alternativa será un elemento de la lista de resultados que contendrá la información de la alternativa
            // y una lista de criterios con sus valores correspondientes
            var matricesAgrupadas = matrices.GroupBy(m => m.IdAlternativa);

            foreach (var grupoMatriz in matricesAgrupadas)
            {
                // Obtener la alternativa actual
                var alternativa = alternativas.FirstOrDefault(a => a.Id == grupoMatriz.Key);

                // Si la alternativa no existe, continuar con la siguiente (sale de la iteracion del ciclo)
                if (alternativa == null) continue;


                // Crear un objeto DTO para la alternativa con todos sus detalles
                AlternativaWithDetailsDTO alternativaDTO = new AlternativaWithDetailsDTO
                {
                    IdAlternativa = alternativa.Id,
                    IdProyecto = alternativa.IdProyecto,
                    Nombre = alternativa.Nombre,
                    Descripcion = alternativa.Descripcion!,
                    Criterios = new List<CriterioWithValorDTO>() //Lista vacía, será llenada en el siguiente ciclo
                };

                foreach (var matriz in grupoMatriz)
                {
                    var criterio = criterios.FirstOrDefault(c => c.Id == matriz.IdCriterio);

                    if (criterio != null)
                    {
                        CriterioWithValorDTO criterioDTO = new CriterioWithValorDTO
                        {
                            Id = criterio.Id,
                            Nombre = criterio.Nombre,
                            Descripcion = criterio.Descripcion!,
                            Peso = criterio.Peso,
                            Valor = matriz.Valor
                        };

                        // Se agregará un elemento por cada criterio registrado en el proyecto
                        alternativaDTO.Criterios.Add(criterioDTO);
                    }
                }

                // Se agregará un elemento por cada alternativa registrada en el proyecto
                resultados.Add(alternativaDTO);                
            }

            return Ok(resultados);

        }


        // Crear un nuevo registro en una matriz, para ello debe proporcionar (necesariamente):
        // IdProyecto (id existente), IdAlternativa (id existente), IdCriterio (id existente) y Valor.
        // POST: api/Matrices
        [HttpPost]
        public async Task<ActionResult<MatrizDTO>> Post(MatrizDTO _matriz)
        {
            var matriz = new Matriz
            {
                IdProyecto = _matriz.IdProyecto,
                IdAlternativa = _matriz.IdAlternativa,
                IdCriterio = _matriz.IdCriterio,
                Valor = _matriz.Valor
            };

            _context.Matrices.Add(matriz);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { idProyecto = matriz.IdProyecto, idAlternativa = matriz.IdAlternativa, idCriterio = matriz.IdCriterio }, matriz.Adapt<MatrizDTO>());
        }
    }
}
