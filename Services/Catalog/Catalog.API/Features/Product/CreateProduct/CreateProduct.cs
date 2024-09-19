using System.ComponentModel.DataAnnotations;
namespace Catalog.API.Features.Product.CreateProduct;
public sealed partial class CreateProduct
{
    public sealed class CreateProductEndPointRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [Required]
        public string ImageFile { get; set; }
        [Required]
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }

    public sealed class CreateProductEndPointResponse
    {
        public long Id { get; set; }
    }
}