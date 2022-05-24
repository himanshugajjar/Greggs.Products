using Greggs.Core.Enums;
using Greggs.Products.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Greggs.Products.Api.Services
{
    public interface IProductService
    {
        Task<ProductDataViewModel> GetProducts(int pageStart, int pageSize, Currency currency);
    }
}
