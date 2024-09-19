namespace Catalog.API.Features.Category.GetCategory;

public sealed partial class GetCategory
{
    public sealed class GetEndPointResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}