using MongoDB.Bson.Serialization.Attributes;

namespace Basket.API.Entities;

public class BaseEntity
{
    public string Id { get; set; }
}