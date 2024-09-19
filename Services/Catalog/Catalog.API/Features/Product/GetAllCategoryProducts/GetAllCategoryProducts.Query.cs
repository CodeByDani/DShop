using Catalog.API.Enums;

namespace Catalog.API.Features.Product.GetAllCategoryProducts;

public sealed partial class GetAllCategoryProducts
{
    public sealed class ReqQuery : IQuery<ResQuery>
    {
        public long CategoryId { get; set; }
        public SortDirection SortDirection { get; set; }
        public int PageIndex { get; set; } = 0;
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? 10 : value;
        }
    }
    public sealed class ResQuery
    {
        public IReadOnlyList<GetAllCategoryProductsCommandRes> Products { get; set; }
        public int TotalCount { get; set; }
    }

    public sealed class GetAllCategoryProductsCommandRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }
}