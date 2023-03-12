using AutoMapper;
using SecPortal.Entities.Infrastructures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SecPortal.Services.Infrastructures
{
    /// <summary>
    /// Create Update Delete Get functions
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class CrudService<TModel> : AutoMapperBase<TModel>, ICrudService<TModel> where TModel : class, IBaseEntities<int>
    {
        private readonly DbSet<TModel> _repository;

        // public IErrorContainer ErrorContainer { get; set; }

        public CrudService(IMapper mapper, DbSet<TModel> repository) : base(mapper)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TModel>> GetAllAsync(params Expression<Func<TModel, object>>[] includes)
        {
            return GetAllAsync(e => true, includes);
        }

        public virtual async Task<TModel> GetAsync(int id)
        {
            return await _repository.FindAsync(id);
        }

        public virtual Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includes)
        {
            return GetQueryable(includes).SingleOrDefaultAsync(predicate);
        }

        public virtual Task<IEnumerable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includes)
        {
            IEnumerable<TModel> entities = GetQueryable(includes).Where(predicate);
            return Task.FromResult(entities);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            if (entity == null) return;

            await DeleteAsync(entity);
        }

        protected IQueryable<TModel> GetQueryable(params Expression<Func<TModel, object>>[] includes)
        {
            var query = _repository.AsQueryable();

            if (includes != null && includes.Count() > 0)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }

        public TModel Add(TModel item)
        {
            return _repository.Add(item).Entity;
        }

        public virtual Task DeleteAsync(TModel item)
        {
            return Task.Run(() => _repository.Remove(item));
        }

        public async Task DeleteAllAsync(Expression<Func<TModel, bool>> predicate)
        {
            var deletions = await GetAllAsync(predicate, null);
            deletions.ToList().ForEach(p => DeleteAsync(p));
        }

        public TModel Get(Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includes)
        {
            return GetQueryable(includes).SingleOrDefault(predicate);
        }

        public IEnumerable<TModel> GetAll(Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includes)
        {
            if (predicate == null)
            {
                return GetAll(includes);
            }
            else
            {
                return GetQueryable(includes).Where(predicate);
            }
        }

        public IEnumerable<TModel> GetAll(params Expression<Func<TModel, object>>[] includes)
        {
            return GetQueryable(includes);
        }

        public TModel Get(int id)
        {
            return _repository.Find(id);
        }

        public IEnumerable<TModel> GetAllSorted(Expression<Func<TModel, bool>> predicate, string orderBy = "", int pageNo = 1, int recordsPerPage = 20, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query;
            if (predicate == null)
            {
                query = GetQueryable(includes);
            }
            else
            {
                query = GetQueryable(includes).Where(predicate);
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            if (pageNo < 1)
            {
                pageNo = 1;
            }

            return query.Skip((pageNo - 1) * recordsPerPage).Take(recordsPerPage);
        }

        public IEnumerable<TModel> GetAllSorted(string predicate, string orderBy = "", int pageNo = 1, int recordsPerPage = 20, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query;
            if (predicate == null)
            {
                query = GetQueryable(includes);
            }
            else
            {
                query = GetQueryable(includes).Where(predicate);
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            if (pageNo < 1)
            {
                pageNo = 1;
            }

            return query.Skip((pageNo - 1) * recordsPerPage).Take(recordsPerPage);
        }

        public int GetAllSortedCount(Expression<Func<TModel, bool>> predicate, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query;
            if (predicate == null)
            {
                query = GetQueryable(includes);
            }
            else
            {
                query = GetQueryable(includes).Where(predicate);
            }

            return query.Count();
        }

        public int GetAllSortedCount(string predicate, params Expression<Func<TModel, object>>[] includes)
        {
            IQueryable<TModel> query;
            if (predicate == null)
            {
                query = GetQueryable(includes);
            }
            else
            {
                query = GetQueryable(includes).Where(predicate);
            }

            return query.Count();
        }
    }
}



