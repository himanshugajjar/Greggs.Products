using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Greggs.Core.Enums
{
    public enum ErrorCode
    {
        [Description("Error due to unknown reason")] // error message for end user
        [Display(Name = "UNKNOWN_REASON")] // code display name for end user
        UnknownReason = 1000,

        [Description("Fail to persist data")]
        [Display(Name = "UNABLE_TO_PERSIST")]
        UnableToPersist = 1001,

        [Description("Fail to retrieve data")]
        [Display(Name = "FAIL_TO_GET_DATA")]
        FailToGetData = 1002,

        [Description("Invalid parameter in request")]
        [Display(Name = "INVALID_REQUEST_PARAMETERS")]
        InvalidRequestParameters = 1003,

        [Description("Data not found")]
        [Display(Name = "DATA_NOT_FOUND")]
        DataNotFound = 1004,

        [Description("Unable to perform operation")]
        [Display(Name = "OPERATION FAILED")]
        OperationFailed = 1005,
    }
}
