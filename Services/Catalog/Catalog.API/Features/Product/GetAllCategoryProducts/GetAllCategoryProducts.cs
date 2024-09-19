using Catalog.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Features.Product.GetAllCategoryProducts;

public sealed partial class GetAllCategoryProducts
{
    public sealed class GetAllCategoryProductsEndPointRequest
    {
        [Required]
        public SortDirection SortDirection { get; set; }
        [Required]
        public int PageIndex { get; set; }
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? 10 : value;
        }
    }

    public sealed class GetAllCategoryProductsEndPointResponse
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<GetAllCategoryProductsEndPointRes> Products { get; set; }
    }

    public sealed class GetAllCategoryProductsEndPointRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }
}