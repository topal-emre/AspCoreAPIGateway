using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Tulpar.CustomerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Get")]
        public IEnumerable<string> Get()
        {
            return Summaries;
        }


        [HttpPost("Post")]
        public IEnumerable<string> Post()
        {
            return Summaries;
        }


    }
}
