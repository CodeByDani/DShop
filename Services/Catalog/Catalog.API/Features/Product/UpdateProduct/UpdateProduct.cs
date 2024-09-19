using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Features.Product.UpdateProduct;

public sealed partial class UpdateProduct
{
    public sealed class UpdateProductEndPointRequest
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

    public sealed class UpdateProductEndPointResponse
    {
        public long Id { get; set; }
    }
}