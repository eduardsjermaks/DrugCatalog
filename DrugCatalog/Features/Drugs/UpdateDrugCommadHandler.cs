using AutoMapper;
using DrugCatalog.Data;
using DrugCatalog.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DrugCatalog.Features.Drugs
{

    public record UpdateDrugCommand : IRequest<DrugDTO>
    {
        public int Id { get; init; }

        public UpdateDrugSnapshot Snapshot { get; init; }
    }

    public record UpdateDrugSnapshot
    {
        public string Code { get; init; }

        public string Label { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }
    }


    public class UpdateDrugCommadHandler : IRequestHandler<UpdateDrugCommand, DrugDTO>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IMapper _mapper;

        public UpdateDrugCommadHandler(DrugCatalogContext drugCatalogContext, IMapper mapper)
        {
            _drugCatalogContext = drugCatalogContext;
            _mapper = mapper;
        }

        public async Task<DrugDTO> Handle(UpdateDrugCommand request, CancellationToken cancellationToken)
        {
            var drugToUpdate = await _drugCatalogContext.FindAsync<Drug>(request.Id);
            if (drugToUpdate == null)
                throw new InvalidOperationException("Drug not found");

            _mapper.Map(request.Snapshot, drugToUpdate);
            await _drugCatalogContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DrugDTO>(drugToUpdate);
        }
    }
}
