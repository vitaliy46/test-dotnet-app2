using aiPeopleTracker.Business.Api.Entity;
using aiPeopleTracker.Business.Api.Services.Crud;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.Api.Repositories;
using AutoMapper;
using NLog;

namespace aiPeopleTracker.Business.Services.Crud
{
    public class PersonCrudService : CrudServiceBase<Person, int, IPersonRepository, PersonDto>, IPersonCrudService
    {
        private IPersonTagRepository _personTagRepository;

        public PersonCrudService(IPersonRepository repository, 
            IPersonTagRepository personTagRepository) : base(repository)
        {
            _personTagRepository = personTagRepository;
        }
        public Person AddTag(int personId, int tagId)
        {
            var tag = _personTagRepository.GetById(tagId);
            var person = Repository.GetById(personId);
            person.Tags.Add(tag);

            person = Repository.Update(person);

            return Mapper.Map<Person>(person);
        }

        public Person DeleteTag(int personId, int tagId)
        {
            var person = Repository.GetById(personId);
            var tag = _personTagRepository.GetById(tagId);
            person.Tags.Remove(tag);
            person = Repository.Update(person);

            return Mapper.Map<Person>(person);
        }
    }
}