using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.EfConfigurations
{
    public class PersonTagMap : BaseEntityMap<PersonTagDto, int>
    {
        public PersonTagMap()
        {
            Property(o => o.Name).IsOptional().HasColumnName("name");

            ToTable("person_tags");
        }
    }
}
