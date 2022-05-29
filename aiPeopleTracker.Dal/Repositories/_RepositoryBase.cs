using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using ATTrade.Data;

namespace aiPeopleTracker.Dal.Repositories
{
    public abstract class RepositoryBase<D, K> : IRepository<D, K>, ICreateSupport<D>, IUpdateSupport<D>, IDeleteSupport<K>
        where D : DtoBase<K>

    {
        protected AiPeopleContext Connection;
        public virtual DbSet<D> Table => Connection.Set<D>();

        public RepositoryBase(AiPeopleContext connection)
        {
            Connection = connection;
        }

        public virtual IQueryable<D> All
        {
            get { return Table.AsQueryable(); }
        }

        public virtual D GetById(K id)
        {
            D entity = Table.FirstOrDefault(CreateEqualityExpressionForId(id));

            return entity;
        }

        public virtual D Create(D entity)
        {
            entity.CreateDate = entity.UpdateDate = DateTime.Now;

            var result = Table.Add(entity);

            Connection.SaveChanges();

            return result;
        }
     
        public virtual D Update(D entity)
        {
            entity.UpdateDate = DateTime.Now;

            var entry = Connection.Entry<D>(entity);

            var original = Table.AsNoTracking().FirstOrDefault(CreateEqualityExpressionForId(entity.Id));

            entry.State = EntityState.Modified;

            entry.Property(e => e.CreateDate).IsModified = false;

            var result = entry.Entity;

            Connection.SaveChanges();

            return result;
        }

        public virtual void Delete(K id)
        {
            var entity = Table.FirstOrDefault(CreateEqualityExpressionForId(id));

            if (entity != null)
            {
                Connection.GetSet<D, K>().Remove(entity);

                Connection.SaveChanges();
            }
        }

        protected virtual Expression<Func<D, bool>> CreateEqualityExpressionForId(K id)
        {
            var lambdaParam = Expression.Parameter(typeof(D));

            var leftExpression = Expression.PropertyOrField(lambdaParam, "Id");

            Expression<Func<object>> closure = () => id;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);

            var lambdaBody = Expression.Equal(leftExpression, rightExpression);

            return Expression.Lambda<Func<D, bool>>(lambdaBody, lambdaParam);
        }
    }
}
