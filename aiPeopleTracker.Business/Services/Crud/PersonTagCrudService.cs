using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using NLog;

namespace aiPeopleTracker.Business.Services.Crud
{
    public class PersonTagCrudService : CrudServiceBase<PersonTag, int, IPersonTagRepository, PersonTagDto>, IPersonTagCrudService
    {
        public PersonTagCrudService(IPersonTagRepository repository) : base(repository)
        {
            
        }
    }
}