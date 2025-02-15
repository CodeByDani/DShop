﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Basket.API.Entities;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
}