﻿namespace Basket.API.Entities;

public class ShoppingCartItem
{
    public int Quantity { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; }

}