using DrugCatalog.Features.Drugs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrugCatalog.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DrugsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DrugsController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public Task<IReadOnlyList<DrugDTO>> Get([FromQuery] GetListQuery query)
            => _mediator.Send(query);
    }
}
