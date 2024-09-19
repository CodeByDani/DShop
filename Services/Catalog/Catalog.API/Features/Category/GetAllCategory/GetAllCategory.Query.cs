using Catalog.API.Enums;

namespace Catalog.API.Features.Category.GetAllCategory;

public sealed partial class GetAllCategory
{
    public sealed class ReqQuery : IQuery<ResQuery>
    {
    }
    public sealed class ResQuery
    {
        public IReadOnlyList<GetAllCategoriesCommandRes> Categories { get; set; }
        public int TotalCount { get; set; }
    }

    public sealed class GetAllCategoriesCommandRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}