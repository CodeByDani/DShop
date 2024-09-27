
namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class Validator : AbstractValidator<ReqCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required!");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is Required!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is Required!");
            RuleFor(x => x.Price).GreaterThan(1000).WithMessage("Price Must be Greater Than 1000!");
        }
    }
}