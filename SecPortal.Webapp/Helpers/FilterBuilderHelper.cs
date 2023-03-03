using SecPortal.Webapp.CQRS.Infrastructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecPortal.Webapp.Helpers
{
    public class FilterBuilderHelper
    {
        private StringBuilder queryBuilder;
        public string BuildQuery<T>(System.Reflection.PropertyInfo[] propertyInfos,
            T target,
            IEnumerable<string> dateProp = null,
            IEnumerable<string> stringProp = null,
            IEnumerable<string> intProp = null,
            IEnumerable<string> decimalProp = null,
            IEnumerable<string> boolProp = null)
        {
            queryBuilder = new StringBuilder();

            if (dateProp != null)
            {
                BuildQueryFromPropList<DateTime, T>(propertyInfos, dateProp, target);
            }

            if (stringProp != null)
            {
                BuildQueryFromPropList<string, T>(propertyInfos, stringProp, target);
            }

            if (intProp != null)
            {
                BuildQueryFromPropList<int, T>(propertyInfos, intProp, target);
            }

            if (decimalProp != null)
            {
                BuildQueryFromPropList<decimal, T>(propertyInfos, decimalProp, target);
            }

            if (boolProp != null)
            {
                BuildQueryFromPropList<bool, T>(propertyInfos, boolProp, target);
            }

            return queryBuilder.ToString();
        }

        public string BuildQuery<T>(System.Reflection.PropertyInfo[] propertyInfos,
           T target,
           IEnumerable<Tuple<string, string[]>> dateProp = null,
           IEnumerable<Tuple<string, string[]>> stringProp = null,
           IEnumerable<Tuple<string, string[]>> intProp = null,
           IEnumerable<Tuple<string, string[]>> decimalProp = null,
           IEnumerable<Tuple<string, string[]>> boolProp = null)
        {
            queryBuilder = new StringBuilder();

            if (dateProp != null)
            {
                BuildQueryFromPropList<DateTime, T>(propertyInfos, dateProp, target);
            }

            if (stringProp != null)
            {
                BuildQueryFromPropList<string, T>(propertyInfos, stringProp, target);
            }

            if (intProp != null)
            {
                BuildQueryFromPropList<int, T>(propertyInfos, intProp, target);
            }

            if (decimalProp != null)
            {
                BuildQueryFromPropList<decimal, T>(propertyInfos, decimalProp, target);
            }

            if (boolProp != null)
            {
                BuildQueryFromPropList<bool, T>(propertyInfos, boolProp, target);
            }

            return queryBuilder.ToString();
        }

        private void BuildQueryFromPropList<TFilterType, TTarget>(System.Reflection.PropertyInfo[] propertyInfos, IEnumerable<string> props, TTarget target)
        {
            foreach (var propName in props)
            {
                string singleQuery;
                singleQuery = ((FilterOperator<TFilterType>)propertyInfos.Single(x => x.Name == propName).GetValue(target, null))?.ToString(propName);

                if (!string.IsNullOrWhiteSpace(singleQuery))
                {
                    if (queryBuilder.Length != 0)
                    {
                        queryBuilder.Append(" AND ");
                    }

                    queryBuilder.Append(singleQuery);
                }
            }
        }

        private void BuildQueryFromPropList<TFilterType, TTarget>(System.Reflection.PropertyInfo[] propertyInfos, IEnumerable<Tuple<string, string[]>> props, TTarget target)
        {
            foreach (var propName in props)
            {
                var holderBuilder = new StringBuilder();
                var temp = (FilterOperator<TFilterType>)propertyInfos.Single(x => x.Name == propName.Item1).GetValue(target, null);

                if (temp == null)
                {
                    continue;
                }

                if (propName.Item2.Length > 1)
                {
                    holderBuilder.Append("(");
                }

                var isFirst = true;
                foreach (var item in propName.Item2)
                {
                    if (!isFirst)
                    {
                        holderBuilder.Append(" OR ");
                    }
                    else
                    {
                        isFirst = false;
                    }

                    holderBuilder.Append(temp.ToString(item));
                }

                if (propName.Item2.Length > 1)
                {
                    holderBuilder.Append(")");
                }

                var singleQuery = holderBuilder.ToString();
                if (!string.IsNullOrWhiteSpace(singleQuery))
                {
                    if (queryBuilder.Length != 0)
                    {
                        queryBuilder.Append(" AND ");
                    }

                    queryBuilder.Append(singleQuery);
                }
            }
        }
    }
}
