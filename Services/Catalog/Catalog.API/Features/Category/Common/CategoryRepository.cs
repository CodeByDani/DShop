using Catalog.API.Common;
using Catalog.API.Features.Category.Common.Interfaces;

namespace Catalog.API.Features.Category.Common;

public class CategoryRepository : BaseRepository<Entities.Category>, ICategoryRepository
{
    private readonly IQuerySession _querySession;
    public CategoryRepository(IDocumentSession documentSession, IQuerySession querySession) : base(documentSession, querySession)
    {
        _querySession = querySession;
    }

    public async Task<IReadOnlyList<Entities.Category>> GetAllCategories()
    {
        return await _querySession.Query<Entities.Category>().ToListAsync();
    }
}