using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSS_Scoring.Data;
using DSS_Scoring.Models;
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

        // GET: api/Alternativas
        [HttpGet]
        public IEnumerable<Criterio> Get()
        {
            var criterios = _context.Criterios.ToList();
            return criterios;
        }


    }
}
