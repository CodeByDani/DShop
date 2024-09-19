namespace Catalog.API.Features.Category.UpdateCategory;

public sealed partial class UpdateCategory
{
    public sealed class UpdateEndPointRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class UpdateEndPointResponse
    {
        public long Id { get; set; }
    }
}