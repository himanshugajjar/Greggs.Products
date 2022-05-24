using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Greggs.Core.Enums
{
    public enum Currency
    {
        [Description("GBP")]
        GBP,

        [Description("Euro")]
        Euro,
    }
}
