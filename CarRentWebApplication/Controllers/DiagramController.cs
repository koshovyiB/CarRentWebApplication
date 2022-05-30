using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagramController : ControllerBase
    {
        private readonly DBCarRentContext _context;
        public DiagramController(DBCarRentContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var brands = _context.Brands.Include(c => c.Cars).ToList();
            List<object> braCar = new List<object>();
            braCar.Add(new[] { "Марка", "Кількість автомобілів" });
            foreach (var c in brands)
            {
                braCar.Add(new object[] { c.Name, c.Cars.Count() });

            }
            return new JsonResult(braCar);
        }
    }
}

