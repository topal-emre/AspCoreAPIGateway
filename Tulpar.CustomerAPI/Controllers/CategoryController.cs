using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Tulpar.CustomerAPI.Controllers
{

    public class CategoryController : BaseController
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


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
