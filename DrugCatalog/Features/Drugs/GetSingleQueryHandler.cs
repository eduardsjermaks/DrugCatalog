using AutoMapper;
using AutoMapper.QueryableExtensions;
using DrugCatalog.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace DrugCatalog.Features.Drugs
{

    public record GetSingleQuery: IRequest<DrugDTO>
    {
        public int Id { get; init; }
    }

    public class GetSingleQueryHandler : IRequestHandler<GetSingleQuery, DrugDTO>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IConfigurationProvider _configuration;

        public GetSingleQueryHandler(DrugCatalogContext drugCatalogContext, IConfigurationProvider configuration)
        {
            _drugCatalogContext = drugCatalogContext;
            _configuration = configuration;
        }

        public async Task<DrugDTO> Handle(GetSingleQuery request, CancellationToken cancellationToken) =>
            await _drugCatalogContext.Drugs
            .ProjectTo<DrugDTO>(_configuration)
            .SingleAsync(d => d.Id == request.Id, cancellationToken);
    }
}
