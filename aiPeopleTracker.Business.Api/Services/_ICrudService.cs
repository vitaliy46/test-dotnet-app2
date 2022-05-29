using aiPeopleTracker.Business.Api.Filters;
using aiPeopleTracker.Core.Common;

namespace aiPeopleTracker.Business.Api.Services
{
    public interface IGetListSupport<D>

    {
        SortableObservableCollection<D> GetList(FilterBase filters);
    }

    public interface IGetSingleSupport<D, K>
    {
        D Get(K id);
    }

    public interface ICreateSupport<D>
    {
        D Create(D detailDto);
    }

    public interface IUpdateSupport<D>
    {
        D Update(D detailDto);
    }

    public interface IDeleteSupport<K>
    {
        void Delete(K id);
    }
}
