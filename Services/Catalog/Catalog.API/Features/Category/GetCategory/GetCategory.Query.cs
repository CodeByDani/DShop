using BuildingBlocks.Base;

namespace Catalog.API.Features.Category.GetCategory;

public sealed partial class GetCategory
{
    public sealed class ReqQuery : IQuery<ResQuery>
    {
        public long Id { get; set; }
    }
    public sealed class ResQuery : ResponseBaseService
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}