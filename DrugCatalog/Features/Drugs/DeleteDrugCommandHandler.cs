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
    public record DeleteDrugCommand : IRequest<DrugDTO>
    {
        public int Id { get; init; }
    }
    public class DeleteDrugCommandValidator : AbstractValidator<DeleteDrugCommand>
    {
        public DeleteDrugCommandValidator()
        {
            RuleFor(m => m.Id).NotEmpty();
        }
    }

    public class DeleteDrugCommandHandler : IRequestHandler<DeleteDrugCommand, DrugDTO>
    {
        private readonly DrugCatalogContext _drugCatalogContext;
        private readonly IMapper _mapper;
        private readonly IValidator<DeleteDrugCommand> _validator;

        public DeleteDrugCommandHandler(DrugCatalogContext drugCatalogContext, IMapper mapper, 
            IValidator<DeleteDrugCommand> validator)
        {
            _drugCatalogContext = drugCatalogContext;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<DrugDTO> Handle(DeleteDrugCommand request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new BusinessException(validationResult.ToString());
            }

            var drugToRemove = await _drugCatalogContext.FindAsync<Drug>(request.Id);
            if (drugToRemove == null)
                throw new BusinessException("Drug not found");

            _drugCatalogContext.Remove(drugToRemove);
            await _drugCatalogContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<DrugDTO>(drugToRemove);
        }
    }
}
