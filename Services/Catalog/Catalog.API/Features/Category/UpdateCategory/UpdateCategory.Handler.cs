using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Category.UpdateCategory;

public sealed partial class UpdateCategory
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
            var category = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.CategoryId, cancellationToken);
            if (category == null)
            {
                return Error.NotFound(nameof(CategoryMessages.NotFoundCategory), CategoryMessages.NotFoundCategory);
            }

            category = request.Adapt(category);

            await _repository.Update(category, cancellationToken);

            return new ResCommand { Id = category.Id };
        }
    }
}