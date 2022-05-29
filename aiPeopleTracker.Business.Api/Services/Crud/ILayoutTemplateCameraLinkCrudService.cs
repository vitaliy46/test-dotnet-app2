using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.Crud
{
    public interface ILayoutTemplateCameraLinkCrudService :
       // IGetSingleSupport<LayoutTemplateCameraLink, int>,
        IGetListSupport<LayoutTemplateCameraLink>,
        ICreateSupport<LayoutTemplateCameraLink>,
        IUpdateSupport<LayoutTemplateCameraLink>,
        IDeleteSupport<int>
    {
       
    }
}