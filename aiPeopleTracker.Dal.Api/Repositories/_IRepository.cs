using System.Linq;
using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Api.Repositories
{
    public interface IRepository
    { }

    public interface ICreateSupport<D> : IRepository
    {
        D Create(D dto);
    }

    public interface IRepository<D, K> : IRepository
        where D : DtoBase<K>
    {
        IQueryable<D> All { get; }

        D GetById(K id);
    }
    

    public interface IUpdateSupport<D> : IRepository
    {
        D Update(D dto);
    }

    public interface IDeleteSupport<K> : IRepository
    {
        void Delete(K id);
    }
}
