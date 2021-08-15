using DrugCatalog.Features.Drugs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public Task<IReadOnlyList<DrugDTO>> GetDrugs([FromQuery] GetListQuery query)
            => _mediator.Send(query);

        [HttpGet("{drugId}")]
        public Task<DrugDTO> GetSingleDrug(int drugId)
            => _mediator.Send(new GetSingleQuery() 
            {
                Id = drugId
            });

        [HttpPost]
        public Task<DrugDTO> CreateDrug(CreateDrugCommand command)
            => _mediator.Send(command);

        [HttpPut("{drugId}")]
        public Task<DrugDTO> UpdateDrug(int drugId, UpdateDrugSnapshot snapshot)
            => _mediator.Send(new UpdateDrugCommand()
            {
                Id = drugId,
                Snapshot = snapshot
            });

        [HttpDelete("{drugId}")]
        public Task<DrugDTO> DeleteDrug(int drugId)
            => _mediator.Send(new DeleteDrugCommand()
            {
                Id = drugId
            });
    }
}
