namespace Catalog.API.Features.Category.UpdateCategory;

public sealed partial class UpdateCategory
{
    public sealed class UpdateCategoryEndPointRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class UpdateCategoryEndPointResponse
    {
        public long Id { get; set; }
    }
}