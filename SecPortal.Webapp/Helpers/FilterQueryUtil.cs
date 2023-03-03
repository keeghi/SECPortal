using SecPortal.Commons.Enums;
using SecPortal.Webapp.CQRS.Infrastructures;
using System.Collections.Generic;

namespace SecPortal.Webapp.Helpers
{
    public static class FilterQueryUtil<TCommand>
        where TCommand : IBaseQueries
    {
        public static TCommand ComposeDefaultFilterQuery(TCommand request, int defPageNo, int defRecPerPg, string defSortBy, DirectionEnum defDirection)
        {
            if (request.PageNo < 1)
            {
                request.PageNo = defPageNo;
            }

            if (request.RecordsPerPage < 1)
            {
                request.RecordsPerPage = defRecPerPg;
            }

            if (string.IsNullOrWhiteSpace(request.SortBy))
            {
                request.Sort = new List<Dictionary<string, string>> { new Dictionary<string, string> { { "field", defSortBy }, { "dir", defDirection.ToString() } } };
            }

            return request;
        }
    }
}
