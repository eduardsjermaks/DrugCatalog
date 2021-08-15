using DrugCatalog.Data;
using MediatR;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using DrugCatalog.Entities;

namespace DrugCatalog.Features.Drugs
{

    public class DrugMappingProfile : Profile
    {
        public DrugMappingProfile()
        {
            CreateMap<Drug, DrugDTO>();
            CreateMap<CreateDrugCommand, Drug>();
        }
    }

    public record GetListQuery: IRequest<IReadOnlyList<DrugDTO>>
    {
        public string? Code { get; init; }

        public string? Label { get; init; }
    }

    public record DrugDTO
    {
        public int Id { get; init; }

        public string Code { get; init; }

        public string Label { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }
    }

    public class GetListQueryHandler : IRequestHandler<GetListQuery, IReadOnlyList<DrugDTO>>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IConfigurationProvider _configuration;

        public GetListQueryHandler(DrugCatalogContext drugCatalogContext, IConfigurationProvider configuration)
        {
            _drugCatalogContext = drugCatalogContext;
            _configuration = configuration;
        }

        public async Task<IReadOnlyList<DrugDTO>> Handle(GetListQuery request, CancellationToken cancellationToken) =>
            await _drugCatalogContext.Drugs
            .OrderBy(d => d.Id)
            .ProjectTo<DrugDTO>(_configuration)
            .ToListAsync();
    }
}
