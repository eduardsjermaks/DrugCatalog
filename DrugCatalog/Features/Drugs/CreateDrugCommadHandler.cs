using AutoMapper;
using DrugCatalog.Data;
using DrugCatalog.Entities;
using DrugCatalog.Exceptions;
using FluentValidation;
using MediatR;
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

    public class CreateDrugCommandValidator : AbstractValidator<CreateDrugCommand>
    {
        public CreateDrugCommandValidator()
        {
            RuleFor(m => m.Code).NotNull().Length(1, 30);
            RuleFor(m => m.Label).NotNull().Length(1, 100);
            RuleFor(m => m.Price).NotNull().GreaterThan(0);
        }
    }

    public class CreateDrugCommadHandler: IRequestHandler<CreateDrugCommand, DrugDTO>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDrugCommand> _validator;

        public CreateDrugCommadHandler(DrugCatalogContext drugCatalogContext, IMapper mapper,
              IValidator<CreateDrugCommand> validator)
        {
            _drugCatalogContext = drugCatalogContext;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<DrugDTO> Handle(CreateDrugCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.ToString());
            }

            var drugToAdd = _mapper.Map<Drug>(request);
            await _drugCatalogContext.Drugs.AddAsync(drugToAdd, cancellationToken);
            await _drugCatalogContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DrugDTO>(drugToAdd);
        }
    }
}
