using AutoMapper;
using DrugCatalog.Data;
using DrugCatalog.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DrugCatalog.Features.Drugs
{

    public record CreateDrugCommand : IRequest<DrugDTO>
    {
        public string Code { get; init; }

        public string Label { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }
    }


    public class CreateDrugCommadHandler: IRequestHandler<CreateDrugCommand, DrugDTO>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IMapper _mapper;

        public CreateDrugCommadHandler(DrugCatalogContext drugCatalogContext, IMapper mapper)
        {
            _drugCatalogContext = drugCatalogContext;
            _mapper = mapper;
        }

        public async Task<DrugDTO> Handle(CreateDrugCommand request, CancellationToken cancellationToken)
        {
            var drugToAdd = _mapper.Map<Drug>(request);
            await _drugCatalogContext.Drugs.AddAsync(drugToAdd, cancellationToken);
            await _drugCatalogContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DrugDTO>(drugToAdd);
        }
    }
}
