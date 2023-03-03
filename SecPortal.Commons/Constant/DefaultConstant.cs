using SecPortal.Commons.Enums;
using System.Collections.Generic;

namespace SecPortal.Commons.Constant
{
    public class DefaultConstant
    {
#if DEBUG
        public const string BASE_URL = "http://localhost:8080";
#else
        public const string BASE_URL = "";
#endif

        /*ERROR CONST*/
        public const string DATA_USED = "DATA_USED";
        public const string DUPLICATE_ENTITY = "DUPLICATE_ENTITY";
        public const string GENERIC_ERROR = "GENERIC_ERROR";
        public const string NOT_FOUND = "NOT_FOUND";
        public const string OK = "OK";

        public static List<StatusEnum> NOT_BILLABLE_STATUS = new List<StatusEnum> { StatusEnum.New, StatusEnum.Failed };
        public const int MINIMAL_STOCK = 10;

    }
}
