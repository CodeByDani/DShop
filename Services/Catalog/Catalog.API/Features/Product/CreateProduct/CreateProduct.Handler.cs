using ErrorOr;

namespace Catalog.API.Features.Product.CreateProduct;

public sealed partial class CreateProduct
{
    public sealed class CommandHandler : ICommandHandler<ReqCommand, ResCommand>
    {
        //private readonly IDocumentSession _session;

        //public CommandHandler(IDocumentSession session)
        //{
        //    _session = session;
        //}

        public async Task<ErrorOr<ResCommand>> Handle(ReqCommand request, CancellationToken cancellationToken)
        {
            //var product = request.Adapt<Entities.Product>();
            //if (product == null)
            //{
            //    return Error.Conflict("Hello", "dai");
            //}

            //_session.Store(product);
            //await _session.SaveChangesAsync(cancellationToken);
            await Task.CompletedTask;
            return new ResCommand { Id = 2 };
        }
    }
}