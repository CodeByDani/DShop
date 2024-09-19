namespace Catalog.API.Features.Category.CreateCategory;
public sealed partial class CreateCategory
{
    public sealed class CreateEndPointRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class CreateEndPointResponse
    {
        public long Id { get; set; }
    }
}