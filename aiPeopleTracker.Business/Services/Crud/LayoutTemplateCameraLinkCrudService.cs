using System;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Filters;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Business.Collections;
using aiPeopleTracker.Core.Common;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using NLog;

namespace aiPeopleTracker.Business.Services.Crud
{
    public class LayoutTemplateCameraLinkCrudService : CrudServiceBase<LayoutTemplateCameraLink, int, ILayoutTemplateCameraLinkRepository, LayoutTemplateCameraLinkDto>, ILayoutTemplateCameraLinkCrudService
    {
        private readonly ICameraCrudService _cameraCrudService;

        public LayoutTemplateCameraLinkCrudService(
            ILayoutTemplateCameraLinkRepository repository, 
           ICameraCrudService cameraCrudService) : base(repository)
        {
            _cameraCrudService = cameraCrudService ?? throw new ArgumentNullException(nameof(cameraCrudService));
        }

        public override SortableObservableCollection<LayoutTemplateCameraLink> GetList(FilterBase filters)
        {
            var list =  base.GetList(filters);

            list.ForEach(x=> x.Camera = _cameraCrudService.Get(x.CameraId));

            return list;
        }
    }
}