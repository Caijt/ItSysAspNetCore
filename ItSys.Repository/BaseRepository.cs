using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using ItSys.Repository.Sqlsugar;
using SqlSugar;

namespace ItSys.Repository
{
    public abstract class BaseRepository<TEntity> where TEntity : class, new()
    {
        protected readonly DbContext dbContext;
        public BaseRepository()
        {
            dbContext = new DbContext();
        }
        public virtual int Add(TEntity data)
        {
            return dbContext.Db.Insertable<TEntity>(data).ExecuteReturnIdentity();
        }

        public virtual bool Update(TEntity data)
        {
            return dbContext.Db.Updateable<TEntity>(data).ExecuteCommandHasChange();
        }

        public virtual bool DeleteById(int id)
        {
            return dbContext.Db.Deleteable<TEntity>().ExecuteCommandHasChange();
        }
        public virtual TEntity GetById(int id)
        {
            return dbContext.Db.Queryable<TEntity>().InSingle(id);
        }
        public virtual List<TEntity> GetList()
        {
            return dbContext.Set<TEntity>().GetList();
        }
        public virtual List<TEntity> GetPageList(int pageIndex, int pageSize,out int pageTotal, Expression<Func<TEntity, bool>> whereExpression)
        {
            Expression<Func<TEntity, bool>> a = b => false;
            pageTotal = 0;
            var pageModel = new PageModel()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                PageCount = pageTotal
            };
            return dbContext.Set<TEntity>().GetPageList(whereExpression, pageModel);
        }
    }
}
