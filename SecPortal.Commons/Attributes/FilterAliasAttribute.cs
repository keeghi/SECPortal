using System;

namespace SecPortal.Commons.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FilterAliasAttribute : Attribute
    {
        public string[] Alias;

        public FilterAliasAttribute(string alias)
        {
            Alias = alias.Split(",");
        }
    }
}
