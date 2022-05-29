using System.Linq;
using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Filters;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using NLog;

namespace aiPeopleTracker.Business.Services.Crud
{
    public class CameraCrudService : CrudServiceBase<Camera, int, ICameraRepository, CameraDto>, ICameraCrudService
    {
        public CameraCrudService(ICameraRepository repository) : base(repository) { }

        protected override IQueryable<CameraDto> DoFiltration(IQueryable<CameraDto> query, FilterBase filters)
        {
            var f = filters as CameraFilter;

            if (f == null) return query;

            if (f.State != null)
            {
                query = query.Where(x => x.State == (int) f.State);
            }

            return query;
        }
    }
}