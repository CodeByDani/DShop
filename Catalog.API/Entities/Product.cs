﻿namespace Catalog.API.Entities;

public sealed class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public decimal Price { get; set; }

    public List<Category> Categories { get; set; }
}