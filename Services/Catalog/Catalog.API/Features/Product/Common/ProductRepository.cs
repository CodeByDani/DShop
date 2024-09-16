using Catalog.API.Common;
using Catalog.API.Features.Product.Common.Interfaces;

namespace Catalog.API.Features.Product.Common;

public class ProductRepository:BaseRepository<Entities.Product>,IProductRepository
{
    public ProductRepository(IDocumentSession documentSession, IQuerySession querySession) : base(documentSession, querySession)
    {

    }
}