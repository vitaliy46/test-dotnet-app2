using System;
using System.Security.Cryptography.X509Certificates;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.BusinessLogic;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using NLog;

namespace aiPeopleTracker.Business.Services.Crud
{
    public class FloorPlanCrudService : CrudServiceBase<FloorPlan, int, IFloorPlanRepository, FloorPlanDto>, IFloorPlanCrudService
    {
        private IFileService _fileService;

        public FloorPlanCrudService(IFloorPlanRepository repository, 
            IFileService fileService) : base(repository)
        {
            _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }
        /// <summary>
        /// Служит для просмотра плана на формах с обним изображением
        /// на котором нанесены камеры
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override FloorPlan Get(int id)
        {
            var dto = Repository.GetById(id);

            var entity = Mapper.Map<FloorPlan>(dto);

            entity.PictureWithCameras = _fileService.ReadFile(dto.UriWithCameras);

            return entity;

        }

        /// <summary>
        /// Получение плана с двумя изображениями, для целей редактирования плана
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FloorPlan GetWithTwoPicture(int id)
        {
            var dto = Repository.GetById(id);

            var entity = Mapper.Map<FloorPlan>(dto);

            entity.PictureWithCameras = _fileService.ReadFile(dto.UriWithCameras);

            entity.Picture = _fileService.ReadFile(dto.Uri);

            return entity;
        }
    }
}