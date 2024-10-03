using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;

namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class CommandHandler : BaseCommandHandler<ReqCommand, ResCommand>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public CommandHandler(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        protected override async Task<ResCommand> HandleCore(ReqCommand request, CancellationToken cancellationToken)
        {
            var product = request.Adapt<Entities.Product>();
            if (product == null)
            {
                return Failure(Error.Unexpected(
                    nameof(ProductMessages.InvalidRequest),
                    ProductMessages.InvalidRequest
                ));
            }
            var category =
              await _categoryRepository.FirstOrDefaultAsync(p => p.Id == request.CategoryId, cancellationToken);
            if (category == null)
            {
                return Failure(Error.NotFound(
                    nameof(CategoryMessages.NotFoundCategory),
                    CategoryMessages.NotFoundCategory));
            }
            product.Category = category;
            await _repository.Store(product, cancellationToken);

            return new ResCommand { Id = product.Id };
        }


    }
}