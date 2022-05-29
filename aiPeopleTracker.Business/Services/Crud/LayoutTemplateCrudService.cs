using System;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Filters;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using NLog;

namespace aiPeopleTracker.Business.Services.Crud
{
    public class LayoutTemplateCrudService : CrudServiceBase<LayoutTemplate, int, ILayoutTemplateRepository, LayoutTemplateDto>,
        ILayoutTemplateCrudService
    {
        private ILayoutTemplateCameraLinkCrudService _layoutTemplateCameraLinkCrudService;

        public LayoutTemplateCrudService(
            ILayoutTemplateRepository repository,
            ILayoutTemplateCameraLinkCrudService layoutTemplateCameraLinkCrudService) : base(repository)
        {
            _layoutTemplateCameraLinkCrudService = layoutTemplateCameraLinkCrudService ?? throw new ArgumentNullException(nameof(layoutTemplateCameraLinkCrudService));
        }
       
        public override LayoutTemplate Get(int id)
        {
            var entity = base.Get(id);

            //Увеличивается приоритет в рейтенге частоты использования шаблонов 
            entity.UsageIndex++;

            entity = Update(entity);

            entity.CameraLinks = _layoutTemplateCameraLinkCrudService.GetList(
                new LayoutTemplateCameraLinkFilter {LayoutTemplateId = entity.Id});

            return entity;
        }
    }
}