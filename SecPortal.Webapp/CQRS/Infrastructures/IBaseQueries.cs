using SecPortal.Commons.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecPortal.Webapp.CQRS.Infrastructures
{
    public class SortQuery
    {
        public string Field { get; set; }
        public DirectionEnum Dir { get; set; }
    }

    public interface IBaseQueries
    {
        int PageNo { get; set; }
        int RecordsPerPage { get; set; }
        [ModelBinder(typeof(QueryStringDictSyntaxBinder<List<SortQuery>>))]
        List<Dictionary<string, string>> Sort { get; set; }
        string SortBy { get; }
    }

    internal class QueryStringDictSyntaxBinder<TModel> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));
            try
            {
                var result = Activator.CreateInstance<TModel>();
                foreach (var pi in typeof(TModel).GetProperties())
                {
                    var modelName = bindingContext.ModelName;
                    var qsFieldName = $"{modelName}[{pi.Name}]";
                    var field = bindingContext.HttpContext.Request.Query[qsFieldName].FirstOrDefault();
                    if (field != null)
                    {
                        pi.SetValue(result, field);
                    }
                    // do nothing if null , or add model binding failure messages if you like
                }
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            catch
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }

    public abstract class BaseQueries : IBaseQueries
    {
        public int PageNo { get; set; }
        public int RecordsPerPage { get; set; }
        public List<Dictionary<string, string>> Sort { get; set; }
        public string SortBy
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var item in Sort)
                {
                    if(item.Count== 0)
                    {
                        continue;
                    }

                    if (sb.Length != 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append($"{item["field"]} {item["dir"]}");
                }

                return sb.ToString();
            }
        }

        public BaseQueries()
        {
            PageNo = 1;
            RecordsPerPage = 20;
            Sort = new List<Dictionary<string, string>>();
        }
    }
}
