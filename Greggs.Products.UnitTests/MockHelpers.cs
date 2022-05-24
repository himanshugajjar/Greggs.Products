using Greggs.Products.Api.Models;
using System.Collections.Generic;

namespace Greggs.Products.UnitTests
{
    public static class MockHelpers
    {
        public static readonly List<Product> ExpectedProducts = new List<Product>()
        {
            new() { Name = "Sausage Roll", PriceInPounds = 1m },
            new() { Name = "Vegan Sausage Roll", PriceInPounds = 1.1m },
            new() { Name = "Steak Bake", PriceInPounds = 1.2m },
            new() { Name = "Yum Yum", PriceInPounds = 0.7m },
            new() { Name = "Pink Jammie", PriceInPounds = 0.5m },
            new() { Name = "Mexican Baguette", PriceInPounds = 2.1m },
            new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m },
            new() { Name = "Coca Cola", PriceInPounds = 1.2m },
            new() { Name = "Bacon Sandwich", PriceInPounds = 1.95m },
            new() { Name = "Roast Chicken and Bacon Club Baguette", PriceInPounds = 3.8m },
            new() { Name = "Tuna Crunch Baguette", PriceInPounds = 3.95m },
            new() { Name = "White Coffee", PriceInPounds = 1.24m },
            new() { Name = "Vegan Ham and Cheeze Sandwich", PriceInPounds = 2.95m },
            new() { Name = "Americano", PriceInPounds = 1.5m }
        };
    }
}