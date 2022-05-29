using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.EfConfigurations
{
    public class PersonMap : BaseEntityMap<PersonDto, int>
    {
        public PersonMap()
        {
            Property(o => o.Name).IsRequired().HasColumnName("name");
            Property(o => o.Surname).IsRequired().HasColumnName("surname");
            Property(o => o.Patronymic).IsRequired().HasColumnName("patronymic");
            Property(o => o.Photo).IsOptional().HasColumnName("photo");
            Property(o => o.Description).IsOptional().HasColumnName("description");

            HasMany(o => o.Tags).WithMany().Map(
                x=> x.MapLeftKey("person_id")
                .MapRightKey("person_tag_id")
                .ToTable("person_person_tag_links"));

            ToTable("persons");
        }
    }
}
