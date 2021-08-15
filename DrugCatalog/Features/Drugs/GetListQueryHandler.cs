using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DrugCatalog.Features.Drugs
{

    public record GetListQuery: IRequest<IReadOnlyList<DrugDTO>>
    {
        public string? Code { get; init; }

        public string? Label { get; init; }
    }

    public record DrugDTO
    {
        public string Code { get; init; }

        public string Label { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }
    }

    public class GetListQueryHandler : IRequestHandler<GetListQuery, IReadOnlyList<DrugDTO>>
    {
        public Task<IReadOnlyList<DrugDTO>> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult((IReadOnlyList<DrugDTO>)Array.Empty<DrugDTO>());
        }
    }
}
