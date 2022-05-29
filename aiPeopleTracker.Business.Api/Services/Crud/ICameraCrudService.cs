using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.Crud
{
    public interface ICameraCrudService :
        IGetSingleSupport<Camera, int>,
        IGetListSupport<Camera>,
        ICreateSupport<Camera>,
        IUpdateSupport<Camera>,
        IDeleteSupport<int>
    {
       
    }
}