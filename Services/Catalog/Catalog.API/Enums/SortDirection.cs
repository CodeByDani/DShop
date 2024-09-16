using Microsoft.OpenApi.Attributes;

namespace Catalog.API.Enums
{
    public enum SortDirection
    {
        [Display("Ascending")]
        Ascending,
        [Display("Descending")]
        Descending
    }
}
