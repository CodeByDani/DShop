namespace Basket.API.Entities;

public class ShoppingCart : BaseEntity
{
    public string UserName { get; set; }
    public ICollection<ShoppingCartItem> Items { get; set; }
    public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

    public ShoppingCart(string userName)
    {
        UserName = userName;
    }
    public ShoppingCart()
    {

    }
}