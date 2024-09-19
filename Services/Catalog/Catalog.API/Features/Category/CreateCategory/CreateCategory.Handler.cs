using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;

namespace Catalog.API.Features.Category.CreateCategory;

public sealed partial class CreateCategory
{
    public sealed class CommandHandler : ICommandHandler<ReqCommand, ResCommand>
    {
        private readonly ICategoryRepository _repository;

        public CommandHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ErrorOr<ResCommand>> Handle(ReqCommand request, CancellationToken cancellationToken)
        {
            var category = request.Adapt<Entities.Category>();
            if (category == null)
            {
                return Error.NotFound(nameof(CategoryMessages.NotFoundCategory), CategoryMessages.NotFoundCategory);
            }
            await _repository.Store(category, cancellationToken);

            return new ResCommand { Id = category.Id };
        }
    }
}