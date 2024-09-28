using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DSS_Scoring.Data;
using DSS_Scoring.Models;
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
        public IEnumerable<Alternativa> Get()
        {
            var alternativas = _context.Alternativas.ToList();
            return alternativas;
        }

        
    }
}
