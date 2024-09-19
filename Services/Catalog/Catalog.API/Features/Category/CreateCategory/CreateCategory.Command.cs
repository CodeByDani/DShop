namespace Catalog.API.Features.Category.CreateCategory;

public sealed partial class CreateCategory
{
    public sealed class ReqCommand : ICommand<ResCommand>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public sealed class ResCommand
    {
        public long Id { get; set; }
    }
}