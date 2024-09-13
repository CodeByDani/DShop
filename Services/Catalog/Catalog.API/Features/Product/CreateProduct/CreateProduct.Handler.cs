using ErrorOr;

namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class CommandHandler : ICommandHandler<ReqCommand, ResCommand>
    {
        private readonly IDocumentSession _session;

        public CommandHandler(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<ErrorOr<ResCommand>> Handle(ReqCommand request, CancellationToken cancellationToken)
        {
            var product = request.Adapt<Entities.Product>();
            if (product == null)
            {
                return Error.Conflict("Sample Error", "Yo We've Got an Error!");
            }

            _session.Store(product);
            await _session.SaveChangesAsync(cancellationToken);
            return new ResCommand { Id = product.Id };
        }
    }
}