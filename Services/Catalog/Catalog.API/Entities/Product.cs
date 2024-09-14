namespace Catalog.API.Entities;

public sealed class Product
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<string> Categories { get; set; }
    public string ImageFile { get; set; }
}