using System;
using aiPeopleTracker.Business.Api.Constants;
using aiPeopleTracker.Business.Api.Data;
using aiPeopleTracker.Business.Api.Filters;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Business.Services.Crud;

namespace aiPeopleTracker.Business.Services.BusinessLogic
{
    public class CameraAppService : ServiceBase, ICameraAppService
    {
        private readonly ICameraCrudService _cameraCrudService;

        public CameraAppService(
           ICameraCrudService cameraCrudService)
        {
            _cameraCrudService = cameraCrudService ?? throw new ArgumentNullException(nameof(cameraCrudService));
        }

        public CamerasByStates GetCamerasCountByStates()
        {
            var result = new CamerasByStates();


            result.CountActive = _cameraCrudService.GetList(new CameraFilter { State = CameraState.Active }).Count;

            result.CountInActive = _cameraCrudService.GetList(new CameraFilter { State = CameraState.InActive }).Count;

            result.Count = result.CountActive + result.CountInActive;


            return result;
        }


    }
}