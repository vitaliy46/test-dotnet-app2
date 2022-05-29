
using aiPeopleTracker.Business.Api.Entity;

namespace aiPeopleTracker.Business.Api.Services.Crud
{
    
    public interface IFloorPlanCrudService : 
        IGetListSupport<FloorPlan>,
        IGetSingleSupport<FloorPlan, int>, 
        ICreateSupport<FloorPlan>,
        IUpdateSupport<FloorPlan>
    {
        /// <summary>
        /// Получение плана с двумя изображениями, для целей редактирования плана
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        FloorPlan GetWithTwoPicture(int id);
    }
}