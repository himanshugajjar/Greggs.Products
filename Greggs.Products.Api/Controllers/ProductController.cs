using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Greggs.Core.Enums;
using Greggs.Core.Models;
using Greggs.Products.Api.Models;
using Greggs.Products.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Greggs.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    /// <summary>
    /// Get list of available Greggs products with base currency detail
    /// </summary>
    /// <remarks>
    /// This will return multiple products and currency detail 
    /// </remarks>
    /// <param name="pageStart"></param>
    /// <param name="pageSize"></param>
    /// <param name="currency"></param>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(ProductDataViewModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult<ProductDataViewModel>> Get(int pageStart = 0, int pageSize = 5, Currency currency = Currency.GBP)
    {
        try
        {
            var errorResponse = new ErrorResponse();

            // this validation can be moved to model validation action filter
            // error messages can be standardised by putting at one place
            if (pageStart < 0)
            {
                errorResponse.Errors.Add(new ErrorModel { ErrorCode = ErrorCode.InvalidRequestParameters, Message = "pageStart: cannot be less than zero." });
            }

            if (pageSize <= 0)
            {
                errorResponse.Errors.Add(new ErrorModel { ErrorCode = ErrorCode.InvalidRequestParameters, Message = "pageSize: must be greater than zero." });
            }

            if (errorResponse.Errors.Any())
            {
                _logger.LogError(errorResponse.ToString());
                return BadRequest(errorResponse);
            }

            var products = await _productService.GetProducts(pageStart, pageSize, currency);

            return Ok(products);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}