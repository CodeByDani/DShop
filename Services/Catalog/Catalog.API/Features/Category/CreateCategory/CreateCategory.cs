namespace Catalog.API.Features.Category.CreateCategory;
public sealed partial class CreateCategory
{
    public sealed class CreateCategoryEndPointRequest
    {
        /// <summary>
        /// CategoryName
        /// </summary>
        /// <example>محصول جدید</example>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        /// <example>توضیحات محصول</example>
        public string Description { get; set; }
    }

    public sealed class CreateCategoryEndPointResponse
    {
        public long Id { get; set; }
    }
}