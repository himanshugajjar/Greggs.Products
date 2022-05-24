using Greggs.Core.Enums;
using Greggs.Core.Models;
using Greggs.Products.Api.Controllers;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductsServiceTests
{
    private Mock<IDataAccess<Product>> _productDataAccessMock;
    private IProductService _productService;

    public ProductsServiceTests()
    {
        _productDataAccessMock = new Mock<IDataAccess<Product>>();
        _productService = new ProductService(_productDataAccessMock.Object);
    }

    //As a Greggs Entrepreneur
    //I want to get the price of the products returned to me in Euros
    //So that I can set up a shop in Europe as part of our expansion

    //Acceptance Criteria
    //The exchange rate we will use can be set as a constant variable and doesn't need to call anything external. For the sake of this story, let's say the exchange rate is 1.11.
    [Fact]
    public void Product_data_must_convert_to_Euro_currency()
    {
        //arrange
        var expectedExchangeRate = ExchangeService.Rates[Currency.Euro];
        _productDataAccessMock.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(MockHelpers.ExpectedProducts);

        //act
        var productData = _productService.GetProducts(0, 15, Currency.Euro).Result;

        //assert
        MockHelpers.ExpectedProducts.ForEach(x =>
        {
            Assert.Contains(productData.Products, p => p.Name.Equals(x.Name) && p.Price == (x.PriceInPounds * expectedExchangeRate));
        });

        Assert.Equal(Currency.Euro, productData.Currency);
        Assert.Equal(expectedExchangeRate, productData.ExchangeRate);
    }


}