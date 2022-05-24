using Greggs.Core.Enums;
using System.Collections.Generic;

namespace Greggs.Products.Api.Models
{
    public class ProductDataViewModel
    {
        public List<ProductViewModel> Products { get; set; }

        public decimal ExchangeRate { get; set; }

        public Currency Currency { get; set; }
    }
}
