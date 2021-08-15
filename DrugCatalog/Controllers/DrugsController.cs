using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DrugCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrugsController : ControllerBase
    {
        public DrugsController()
        {
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "ok" };
        }
    }
}
