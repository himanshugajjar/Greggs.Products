using Greggs.Core.Enums;
using Greggs.Products.Api.DataAccess;
using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greggs.Products.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IDataAccess<Product> _productDataAccess;

        public ProductService(IDataAccess<Product> productDataAccess)
        {
            _productDataAccess = productDataAccess;
        }

        public async Task<ProductDataViewModel> GetProducts(int pageStart, int pageSize, Currency currency)
        {
            // assuming product data access layer is eternal/database call and converting it to async to avoid blocking
            var products = await Task.FromResult(_productDataAccess.List(pageStart, pageSize));
            var exchangeRate = ExchangeService.Rates[currency];

            var productData = new ProductDataViewModel
            {
                Products = products.Select(x => MapProductViewModel(x, exchangeRate)).ToList(),
                ExchangeRate = exchangeRate,
                Currency = currency
            };

            return productData;
        }

        private ProductViewModel MapProductViewModel(Product product, decimal exchangeRate)
        {
            return new ProductViewModel
            {
                Name = product.Name,
                Price = product.PriceInPounds * exchangeRate
            };
        }
    }
}
