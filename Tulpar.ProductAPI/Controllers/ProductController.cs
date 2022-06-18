using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Tulpar.ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ProductController> _logger;



        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Summaries;
        }
    }
}
