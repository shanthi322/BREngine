using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Payment
{
    private readonly Product _physicalProduct;
    private decimal _amount;
    private IShippingSlipService _shippingSlipService;
    private string _shippingAddress;

    public Payment(Product physicalProduct, string shippingAddress)
    {
        if (physicalProduct == null) throw new ArgumentNullException("physicalProduct");
        if (shippingAddress == null) throw new ArgumentNullException("shippingAddress");

        this._physicalProduct = physicalProduct;
        this._shippingAddress = shippingAddress;
    }

    public Payment(object physicalProduct1, object shippingAddress)
    {
    }

    public virtual void FullPayment(decimal amount)
    {
        _amount = amount;
        if (_physicalProduct != null) _shippingSlipService.GenerateSSlipForAddress(_physicalProduct.GetShippingAddress());
    }

    public void SetShippingService(IShippingSlipService shippingSlipService)
    {
        _shippingSlipService = shippingSlipService;
    }
}

public interface IShippingSlipService
{
    void GenerateSSlipForAddress(string shippingAddress);
}