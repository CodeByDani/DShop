namespace Catalog.API.Features.Category.DeleteCategory;

public sealed partial class DeleteCategory
{
    public sealed class ReqCommand : ICommand<ResCommand>
    {
        public long Id { get; set; }
    }
    public sealed class ResCommand
    {
    }
}