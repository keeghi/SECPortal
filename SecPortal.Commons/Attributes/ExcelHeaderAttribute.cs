using System;

namespace SecPortal.Commons.Annotations
{
    [System.AttributeUsage(System.AttributeTargets.Property)]
    public class ExcelHeaderAttribute : Attribute
    {
        public string HeaderName;

        public ExcelHeaderAttribute(string headerName)
        {
            HeaderName = headerName;
        }
    }
}
