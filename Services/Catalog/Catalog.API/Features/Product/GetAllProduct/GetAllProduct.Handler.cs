﻿using Catalog.API.Enums;
using Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.Common.Interfaces;
using ErrorOr;

namespace Catalog.API.Features.Product.GetAllProduct;

public sealed partial class GetAllProduct
{
    public sealed class QueryHandler : IQueryHandler<ReqQuery, ResQuery>
    {
        private readonly IProductRepository _repository;

        public QueryHandler(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ErrorOr<ResQuery>> Handle(ReqQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetWithPagination(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                selector: p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Categories = p.Categories,
                    ImageFile = p.ImageFile,
                    Price = p.Price,
                    Description = p.Description
                }, orderBy: p => p.Id,
                isDescending: request.SortDirection != SortDirection.Ascending,
                cancellationToken);
            if (result.Item1 == null)
            {
                return Error.NotFound(nameof(ProductMessages.NotFoundProduct), ProductMessages.NotFoundProduct);
            }

            var products = result.Item1.Adapt<IReadOnlyList<GetAllProductsCommandRes>>();
            var totalCount = result.TotalCount;
            return new ResQuery
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}