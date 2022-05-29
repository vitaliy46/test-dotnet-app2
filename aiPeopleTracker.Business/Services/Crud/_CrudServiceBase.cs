using System.Linq;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Filters;
using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Core.Common;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using NLog;
using Unity;

namespace aiPeopleTracker.Business.Services.Crud
{
    public class ServiceBase
    {
        [Dependency]
        public  IMapper Mapper { get; set; }
        [Dependency]
        public ILogger Logger { get; set; }
    }

    public class CrudServiceBase<TEntity, TKey, TRepository, TDto> : ServiceBase
        where TEntity : EntityBase<TKey>
        where TRepository : IRepository
        where TDto : DtoBase<TKey>
    {
        protected TRepository Repository;

        public CrudServiceBase(TRepository repository)
        {
            Repository = repository;
        }

        public virtual TEntity Get(TKey id)
        {
            var dto = ((Dal.Api.Repositories.IRepository<TDto, TKey>)Repository).GetById(id);

            return Mapper.Map<TEntity>(dto);
        }

        public virtual SortableObservableCollection<TEntity> GetList(FilterBase filters)
        {
            var all = ((Dal.Api.Repositories.IRepository<TDto,TKey>)Repository).All;

            var filtredList = DoFiltration(all, filters);

            var result = new SortableObservableCollection<TEntity>();

            filtredList.ForEach(x => result.Add(Mapper.Map<TEntity>(x)));

            return result;
        }


        public virtual TEntity Create(TEntity entity)
        {
            var dto = Mapper.Map<TDto>(entity);

            var result = ((Dal.Api.Repositories.ICreateSupport<TDto>) Repository).Create(dto);

            return Mapper.Map<TEntity>(result);
        }

        public virtual TEntity Update(TEntity entity)
        {
            var dto = ((Dal.Api.Repositories.IRepository<TDto, TKey>)Repository).GetById(entity.Id);

            Mapper.Map(entity, dto);

            dto = ((Dal.Api.Repositories.IUpdateSupport<TDto>) Repository).Update(dto);

            return Mapper.Map<TEntity>(dto);
        }

        public virtual void Delete(TKey id)
        {
            ((Dal.Api.Repositories.IDeleteSupport<TKey>)Repository).Delete(id);
        }

        protected virtual IQueryable<TDto> DoFiltration(IQueryable<TDto> query, FilterBase filters)
        {
            return query;
        }
    }
}