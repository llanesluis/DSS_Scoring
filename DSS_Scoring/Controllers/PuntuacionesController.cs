using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSS_Scoring.Data;
using DSS_Scoring.Models;
using Microsoft.EntityFrameworkCore;

namespace DSS_Scoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuntuacionesController : ControllerBase
    {
        private MyDbContext _context;
        public PuntuacionesController(MyDbContext context)
        {
            _context = context;
        }

        // GET api/Puntuaciones
        [HttpGet]
        public IEnumerable<Puntuacion> Get()
        {   
            var puntuaciones = _context.Puntuaciones.ToList();
            return puntuaciones;

        }
    }
}
