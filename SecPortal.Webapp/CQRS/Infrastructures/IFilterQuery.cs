using SecPortal.Commons.Attributes;
using SecPortal.Commons.Helpers;
using SecPortal.Webapp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public interface IFilterQuery
    {
        string ConstructQuery();
    }

    public abstract class FilterQuery : BaseQueries, IFilterQuery
    {
        public string ConstructQuery()
        {
            List<Tuple<string, string[]>> dateProp = new List<Tuple<string, string[]>>();
            List<Tuple<string, string[]>> stringProp = new List<Tuple<string, string[]>>();
            List<Tuple<string, string[]>> intProp = new List<Tuple<string, string[]>>();
            List<Tuple<string, string[]>> decimalProp = new List<Tuple<string, string[]>>();
            List<Tuple<string, string[]>> boolProp = new List<Tuple<string, string[]>>();

            var properties = this.GetType().GetProperties();
            foreach (var item in properties.Where(x => x.PropertyType.IsGenericType))
            {
                if (item.PropertyType.IsGenericType && item.PropertyType.GetGenericTypeDefinition() == typeof(FilterOperator<>))
                {
                    var type = item.PropertyType.GenericTypeArguments[0];
                    var alias = new string[] { item.Name };
                    var holder = this.GetAttributeFrom<FilterAliasAttribute>(item.Name);
                    if (holder != null)
                    {
                        alias = holder.Alias;
                    }

                    if (type == typeof(DateTime))
                    {
                        dateProp.Add(new Tuple<string, string[]>(item.Name, alias));
                    }
                    else if (type == typeof(string))
                    {
                        stringProp.Add(new Tuple<string, string[]>(item.Name, alias));
                    }
                    else if (type == typeof(int))
                    {
                        intProp.Add(new Tuple<string, string[]>(item.Name, alias));
                    }
                    else if (type == typeof(decimal))
                    {
                        decimalProp.Add(new Tuple<string, string[]>(item.Name, alias));
                    }
                    else if (type == typeof(double))
                    {
                        decimalProp.Add(new Tuple<string, string[]>(item.Name, alias));
                    }
                    else if (type == typeof(bool))
                    {
                        boolProp.Add(new Tuple<string, string[]>(item.Name, alias));
                    }
                }
            }

            var helper = new FilterBuilderHelper();
            var result = helper.BuildQuery(properties, this, dateProp, stringProp, intProp, decimalProp, boolProp);
            return result;
        }
    }
}
