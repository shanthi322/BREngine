using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessRuleEngine;
using Moq;

namespace BusinessRuleEngine.Tests
{
    [TestClass]
    public class BusinessRuleEngineTest
    {
        private object _physicalProduct;
        private object _shippingServiceMock;
        private object _payment;

        public object PaymentAmount { get; private set; }

        [TestMethod]
        public void PaymentForAPhysicalProductGeneratesAPackagingSlipForShippingTest()
        {
            new Story("payment is for a physical product, generate a packing slip for shipping")
                .InOrderTo("Ship products a packaging slip should be generated")
                .AsA("User")
                .IWant("Packaging slip for shipping my products")
                .WithScenario("Payment for physical product")
                .Given(ProductIsReadyForPayment)
                .When(PaymentIsDoneForProduct)
                .Then(GenerateAShippingSlip)
                .ExecuteWithReport(MethodBase.GetCurrentMethod());
        }

        private void GenerateAShippingSlip()
        {
            _payment.FullPayment(PaymentAmount);
            _shippingServiceMock.Verify((x) => x.GenerateShippingSlipForAddress(_physicalProduct.GetShippingAddress()));
        }

        private void PaymentIsDoneForProduct()
        {
            _payment = new Payment(_physicalProduct, ShippingAddress);
            _shippingServiceMock = new Mock<IShippingSlipService>();

            _payment.SetShippingService(_shippingServiceMock.Object);
        }

        private void ProductIsReadyForPayment()
        {
            _physicalProduct = new Product("IPhone");
            var physicalProduct = _physicalProduct;
            const string shippingAddress = "TestCompany Bangalore";
            physicalProduct.SetShippingAddress(shippingAddress);
        }
    }
 
}