using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Category.DeleteCategory;

public sealed partial class DeleteCategory
{
    public sealed class CommandHandler : BaseCommandHandler<ReqCommand, ResCommand>
    {
        private readonly ICategoryRepository _repository;

        public CommandHandler(ICategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<ResCommand> HandleCore(ReqCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (category == null)
            {
                return Failure(Error.NotFound(nameof(CategoryMessages.NotFoundCategory),
                    CategoryMessages.NotFoundCategory));
            }
            await _repository.Delete(category, cancellationToken);

            return new ResCommand();
        }
    }
}