using AutoMapper;
using DrugCatalog.Data;
using DrugCatalog.Entities;
using DrugCatalog.Exceptions;
using FluentValidation;
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
    public class UpdateDrugSnapshotValidator : AbstractValidator<UpdateDrugSnapshot>
    {
        public UpdateDrugSnapshotValidator()
        {
            RuleFor(m => m.Code).NotNull().Length(1, 30);
            RuleFor(m => m.Label).NotNull().Length(1, 100);
            RuleFor(m => m.Price).NotNull().GreaterThan(0);
        }
    }


    public class UpdateDrugCommadHandler : IRequestHandler<UpdateDrugCommand, DrugDTO>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateDrugSnapshot> _validator;

        public UpdateDrugCommadHandler(DrugCatalogContext drugCatalogContext, IMapper mapper,
              IValidator<UpdateDrugSnapshot> validator)
        {
            _drugCatalogContext = drugCatalogContext;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<DrugDTO> Handle(UpdateDrugCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request.Snapshot);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.ToString());
            }

            var drugToUpdate = await _drugCatalogContext.FindAsync<Drug>(request.Id);
            if (drugToUpdate == null)
                throw new BusinessException("Drug not found");

            _mapper.Map(request.Snapshot, drugToUpdate);
            await _drugCatalogContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DrugDTO>(drugToUpdate);
        }
    }
}
