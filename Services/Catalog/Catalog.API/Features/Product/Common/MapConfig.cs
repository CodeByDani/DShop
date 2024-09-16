namespace Catalog.API.Features.Product.Common;
using Catalog.API.Features.Product.UpdateProduct;
public sealed class MapConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateProduct.ReqCommand, Entities.Product>().
            Ignore(dest => dest.Id);
    }
}