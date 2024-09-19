using Catalog.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Features.Category.GetAllCategory;

public sealed partial class GetAllCategory
{
    public sealed class GetAllEndPointResponse
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<GetAllCategoriesEndPointRes> Categories { get; set; }
    }

    public sealed class GetAllCategoriesEndPointRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}