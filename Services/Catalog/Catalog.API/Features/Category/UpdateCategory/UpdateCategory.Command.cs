using BuildingBlocks.Base;

namespace Catalog.API.Features.Category.UpdateCategory;

public sealed partial class UpdateCategory
{
    public sealed class ReqCommand : ICommand<ResCommand>
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class ResCommand : ResponseBaseService
    {
        public long Id { get; set; }
    }
}