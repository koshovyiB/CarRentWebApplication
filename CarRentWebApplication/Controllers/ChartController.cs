using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly DBCarRentContext _context;
        public ChartController(DBCarRentContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var colors = _context.Colors.Include(c=>c.Cars).ToList();
            List<object> colCar = new List<object>();
            colCar.Add(new[] { "Колір", "Кількість автомобілів" });
            foreach (var c in colors)
            {
                colCar.Add(new object[] { c.Name, c.Cars.Count() });
            
            }
            return new JsonResult(colCar);
        }
    }
}
