namespace Catalog.API.Features.Category.CreateCategory;
public sealed partial class CreateCategory
{
    public sealed class CreateCategoryEndPointRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class CreateCategoryEndPointResponse
    {
        public long Id { get; set; }
    }
}