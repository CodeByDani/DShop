using BuildingBlocks.DependencyInjection;
using Catalog.API.Common.Interfaces;

namespace Catalog.API.Features.Category.Common.Interfaces;

public interface ICategoryRepository : IRepository<Entities.Category>, IScopeLifetime
{
    Task<IReadOnlyList<Entities.Category>> GetAllCategories();
}