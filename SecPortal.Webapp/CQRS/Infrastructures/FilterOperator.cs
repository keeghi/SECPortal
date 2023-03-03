using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class FilterOperator<T>
    {
        public OperatorEnum? Operator { get; set; }
        public List<T> Value { get; set; }

        public FilterOperator()
        {
            Value = new List<T>();
        }

        public FilterOperator(OperatorEnum op, List<T> value)
        {
            Operator = op;
            Value = value;
        }

        public string ToString(string propName)
        {
            if (Operator == null)
            {
                return string.Empty;
            }

            if (typeof(T) == typeof(bool))
            {
                return $"{propName} == {Value[0]}";
            }


            switch (Operator)
            {
                case OperatorEnum.GreaterThan:
                    if (typeof(T) == typeof(string) || typeof(T) == typeof(Guid) || typeof(DateTime) == typeof(T))
                    {
                        return $"{propName} > \"{Value[0]}\"";
                    }
                    else
                    {
                        return $"{propName} > {Value[0]}";
                    }
                case OperatorEnum.LessThan:
                    if (typeof(T) == typeof(string) || typeof(T) == typeof(Guid) || typeof(DateTime) == typeof(T))
                    {
                        return $"{propName} < \"{Value[0]}\"";
                    }
                    else
                    {
                        return $"{propName} < {Value[0]}";
                    }
                case OperatorEnum.GreaterOrEqualThan:
                    if (typeof(T) == typeof(string) || typeof(T) == typeof(Guid))
                    {
                        return $"{propName} >= \"{Value[0]}\"";
                    }
                    else if (typeof(DateTime) == typeof(T))
                    {
                        DateTime startDate = DateTime.Parse(Value[0].ToString());
                        return $"{propName} >= \"{startDate.ToUniversalTime()}\"";
                    }
                    else
                    {
                        return $"{propName} >= {Value[0]}";
                    }
                case OperatorEnum.LessOrEqualThan:
                    if (typeof(T) == typeof(string) || typeof(T) == typeof(Guid))
                    {
                        return $"{propName} <= \"{Value[0]}\"";
                    }
                    else if (typeof(DateTime) == typeof(T))
                    {
                        DateTime endDate = DateTime.Parse(Value[0].ToString());
                        return $"{propName} <= \"{endDate.ToUniversalTime().AddDays(1)}\"";
                    }
                    else
                    {
                        return $"{propName} <= {Value[0]}";
                    }
                case OperatorEnum.Equal:
                    if (typeof(T) == typeof(string) || typeof(T) == typeof(Guid) || typeof(DateTime) == typeof(T))
                    {
                        return $"{propName} == \"{Value[0]}\"";
                    }
                    else
                    {
                        return $"{propName} == {Value[0]}";
                    }
                case OperatorEnum.NotEqual:
                    if (typeof(T) == typeof(string) || typeof(T) == typeof(Guid) || typeof(DateTime) == typeof(T))
                    {
                        return $"{propName} != \"{Value[0]}\"";
                    }
                    else
                    {
                        return $"{propName} != {Value[0]}";
                    }
                case OperatorEnum.Between:
                    if (Value.Count != 2) throw new ArgumentException($"Needs 2 value can be added for BETWEEN operator on {propName}.");
                    if (typeof(T) == typeof(string) || typeof(T) == typeof(Guid))
                    {
                        return $"{propName} >= \"{Value[0]}\" AND {propName} <= \"{Value[1]}\"";
                    }
                    else if (typeof(DateTime) == typeof(T))
                    {
                        DateTime startDate = DateTime.Parse(Value[0].ToString());
                        DateTime endDate = DateTime.Parse(Value[1].ToString());
                        return $"{propName} >= \"{startDate.ToUniversalTime()}\" AND {propName} <= \"{endDate.ToUniversalTime().AddDays(1)}\"";
                    }
                    else
                    {
                        return $"{propName} >= {Value[0]} AND {propName} <= {Value[1]}";
                    }
                case OperatorEnum.Like:
                    return $"{propName}.Contains(\"{Value[0]}\")";
                case OperatorEnum.StartsWith:
                    return $"{propName}.StartsWith(\"{Value[0]}\")";
                case OperatorEnum.EndsWith:
                    return $"{propName}.EndsWith(\"{Value[0]}\")";
                case OperatorEnum.NotNull:
                    return $"{propName} != null";
                default:
                    throw new ArgumentException("Operator is not recognized");
            }
        }
    }

    public enum OperatorEnum
    {
        [Description(">")]
        GreaterThan,
        [Description("<")]
        LessThan,
        [Description(">=")]
        GreaterOrEqualThan,
        [Description("<=")]
        LessOrEqualThan,
        [Description("=")]
        Equal,
        [Description("!=")]
        NotEqual,
        [Description("BETWEEN")]
        Between,
        [Description("LIKE")]
        Like,
        [Description("STARTSWITH")]
        StartsWith,
        [Description("ENDSWITH")]
        EndsWith,
        [Description("NOTNULL")]
        NotNull
    }
}
