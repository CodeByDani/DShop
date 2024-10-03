using BuildingBlocks.Base;
using Catalog.API.Enums;

namespace Catalog.API.Features.Product.GetAllProduct;

public sealed partial class GetAllProduct
{
    public sealed class ReqQuery : IQuery<ResQuery>
    {
        public SortDirection SortDirection { get; set; }
        public int PageIndex { get; set; } = 0;
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? 10 : value;
        }
    }
    public sealed class ResQuery : ResponseBaseService
    {
        public IReadOnlyList<GetAllProductsCommandRes> Products { get; set; }
        public int TotalCount { get; set; }
    }

    public sealed class GetAllProductsCommandRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CategoryModelCommandRes Category { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }

    public sealed class CategoryModelCommandRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}