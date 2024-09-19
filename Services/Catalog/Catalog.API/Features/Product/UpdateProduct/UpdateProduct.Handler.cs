using Catalog.API.Features.Category.Common;
using Catalog.API.Features.Category.Common.Interfaces;
using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;

namespace Catalog.API.Features.Product.UpdateProduct;

public sealed partial class UpdateProduct
{
    public sealed class CommandHandler : ICommandHandler<ReqCommand, ResCommand>
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public CommandHandler(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<ErrorOr<ResCommand>> Handle(ReqCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository
                .FirstOrDefaultAsync(p => p.Id == request.ProductId, cancellationToken);
            if (product == null)
            {
                return Error.NotFound(nameof(ProductMessages.NotFoundProduct), ProductMessages.NotFoundProduct);
            }
            var category =
                await _categoryRepository.FirstOrDefaultAsync(p => p.Id == request.CategoryId, cancellationToken);
            if (category == null)
            {
                return Error.NotFound(nameof(CategoryMessages.NotFoundCategory), CategoryMessages.NotFoundCategory);
            }
            product.Category = category;
            product = request.Adapt(product);

            await _repository.Update(product, cancellationToken);

            return new ResCommand { Id = product.Id };
        }
    }
}