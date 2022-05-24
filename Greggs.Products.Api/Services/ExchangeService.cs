using Greggs.Core.Enums;
using System.Collections.Generic;

namespace Greggs.Products.Api.Services
{
    // I just create static service to reduce implemenation for this coding test
    // this service can call external interface to get rates or internal repository
    public static class ExchangeService
    {
        public static Dictionary<Currency, decimal> Rates => 
            new Dictionary<Currency, decimal>
            { 
                {Currency.GBP, 1m },
                {Currency.Euro, 1.11m },
            };
    }
}
