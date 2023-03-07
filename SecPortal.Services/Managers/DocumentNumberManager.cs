using SecPortal.Entities.Infrastructures;
using SecPortal.Services.Infrastructures;
using System;
using System.Linq;

namespace SecPortal.Services.Managers
{
    public static class DocumentNumberManager<TEntity>
           where TEntity : BaseEntities, IBaseEntities<Guid>, IDocumentNumber
    {
        public static string GetNextDocNumber(ICrudService<TEntity> service, IDocumentNumberWithIdentifier entity)
        {
            throw new NotImplementedException();

            //var targetDate = entity.CreatedAt.Date.Year;
            //var docList = service.GetAllSorted(x => x.Id != entity.Id && x.CreatedAt.Date.Year == targetDate, $"{nameof(entity.DocumentNo)} desc", 1, 1).ToList();
            //var preposition = "INVOICE";
            //var nextNumber = 1;

            //if (docList.Any())
            //{
            //    var targetDoc = docList.First();
            //    if (!string.IsNullOrEmpty(targetDoc.DocumentNo))
            //    {
            //        nextNumber = int.Parse(targetDoc.DocumentNo.Split("-")[2]) + 1;
            //    }
            //}

            //return $"{preposition}-{entity.CreatedAt:yyyy}-{nextNumber.ToString().PadLeft(4, '0')}";
        }
    }

    public enum RomanMonthEnum
    {
        I = 1,
        II = 2,
        III = 3,
        IV = 4,
        V = 5,
        VI = 6,
        VII = 7,
        VIII = 8,
        IX = 9,
        X = 10,
        XI = 11,
        XII = 12
    }
}
