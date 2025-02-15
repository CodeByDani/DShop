﻿using Catalog.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Features.Product.GetAllProduct;

public sealed partial class GetAllProduct
{
    public sealed class GetAllProductEndPointRequest
    {
        [Required]
        public SortDirection SortDirection { get; set; }
        [Required]
        public int PageIndex { get; set; }
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? 10 : value;
        }
    }

    public sealed class GetAllProductEndPointResponse
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<GetAllProductsEndPointRes> Products { get; set; }
    }

    public sealed class GetAllProductsEndPointRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public PGetAllCategoryModelEndPointRes Category { get; set; }
        public string ImageFile { get; set; }
        public Decimal Price { get; set; }
        public string Description { get; set; }
    }

    public sealed class PGetAllCategoryModelEndPointRes
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}