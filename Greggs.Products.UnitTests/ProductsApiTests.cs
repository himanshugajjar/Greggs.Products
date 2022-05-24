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
using System.Linq;
using System.Net;
using Xunit;

namespace Greggs.Products.UnitTests;

public class ProductsApiTests
{
    private Mock<ILogger<ProductController>> _loggerMock;
    private Mock<IDataAccess<Product>> _productDataAccessMock;
    private IProductService _productService;

    public ProductsApiTests()
    {
        _loggerMock = new Mock<ILogger<ProductController>>();
        _productDataAccessMock = new Mock<IDataAccess<Product>>();
        _productService = new ProductService(_productDataAccessMock.Object);
    }

    //As a Greggs Fanatic
    //I want to be able to get the latest menu of products rather than the random static products it returns now
    //So that I get the most recently available products.

    //Acceptance Criteria
    //The api will return the products but it will use the data access functionality previously implemented.
    [Fact]
    public void Data_must_read_from_data_access()
    {
        //arrange
        _productDataAccessMock.Setup(x => x.List(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(MockHelpers.ExpectedProducts);

        var productController = new ProductController(_loggerMock.Object, _productService);

        //act
        var response = productController.Get().Result;

        var result = response.Result as OkObjectResult;
        var productData = result.Value as ProductDataViewModel;

        //assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        MockHelpers.ExpectedProducts.ForEach(x => 
        {
            Assert.Contains( productData.Products, p => p.Name.Equals(x.Name) && p.Price == x.PriceInPounds);
        });

        Assert.Equal(Currency.GBP, productData.Currency);
        Assert.Equal(1m, productData.ExchangeRate);
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-5, -1)]
    [InlineData(-100, 0)]
    public void Invalid_pageSize_and_startPage_should_return_ErrorResponse(int pageStart, int pageSize)
    {
        var expectedErrorResponse = new ErrorResponse
        {
            Errors =
            {
                new ErrorModel { ErrorCode = ErrorCode.InvalidRequestParameters, Message = "pageStart: cannot be less than zero." },
                new ErrorModel { ErrorCode = ErrorCode.InvalidRequestParameters, Message = "pageSize: must be greater than zero." },
            }
        };
        var productController = new ProductController(_loggerMock.Object, _productService);

        //act
        var response = productController.Get(pageStart, pageSize).Result;

        var result = response.Result as BadRequestObjectResult;
        var actualErrorRresponse = result.Value as ErrorResponse;

        //assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(expectedErrorResponse.ToString(), actualErrorRresponse.ToString());
    }
}