using AutoMapper;
using DrugCatalog.Data;
using DrugCatalog.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DrugCatalog.Features.Drugs
{
    public record DeleteDrugCommand : IRequest<DrugDTO>
    {
        public int Id { get; init; }
    }

    public class DeleteDrugCommandHandler : IRequestHandler<DeleteDrugCommand, DrugDTO>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IMapper _mapper;

        public DeleteDrugCommandHandler(DrugCatalogContext drugCatalogContext, IMapper mapper)
        {
            _drugCatalogContext = drugCatalogContext;
            _mapper = mapper;
        }

        public async Task<DrugDTO> Handle(DeleteDrugCommand request, CancellationToken cancellationToken)
        {
            var drugToRemove = await _drugCatalogContext.FindAsync<Drug>(request.Id);
            if (drugToRemove == null)
                throw new InvalidOperationException("Drug not found");

            _drugCatalogContext.Remove(drugToRemove);
            await _drugCatalogContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DrugDTO>(drugToRemove);
        }
    }
}
