using BuildingBlocks.DependencyInjection;
using Catalog.API.Common.Interfaces;

namespace Catalog.API.Features.Product.Common.Interfaces;

public interface IProductRepository:IRepository<Entities.Product>,IScopeLifetime
{
    
}