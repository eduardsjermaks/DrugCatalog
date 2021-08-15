using AutoMapper;
using AutoMapper.QueryableExtensions;
using DrugCatalog.Data;
using DrugCatalog.Exceptions;
using FluentValidation;
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

    public class GetSingleQueryValidator : AbstractValidator<GetSingleQuery>
    {
        public GetSingleQueryValidator()
        {
            RuleFor(m => m.Id).NotEmpty();
        }
    }

    public class GetSingleQueryHandler : IRequestHandler<GetSingleQuery, DrugDTO>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IConfigurationProvider _configuration;
        private readonly IValidator<GetSingleQuery> _validator;

        public GetSingleQueryHandler(DrugCatalogContext drugCatalogContext, IConfigurationProvider configuration,
              IValidator<GetSingleQuery> validator)
        {
            _drugCatalogContext = drugCatalogContext;
            _configuration = configuration;
            _validator = validator;
        }

        public async Task<DrugDTO> Handle(GetSingleQuery request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.ToString());
            }

            return await _drugCatalogContext.Drugs
                .ProjectTo<DrugDTO>(_configuration)
                .SingleAsync(d => d.Id == request.Id, cancellationToken);
        }
    }
}
