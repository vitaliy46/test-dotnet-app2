using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.EfConfigurations
{
    public class CameraMap : BaseEntityMap<CameraDto, int>
    {
        public CameraMap()
        {
            Property(o => o.Name).IsRequired().HasColumnName("name");
            Property(o => o.Number).IsRequired().HasColumnName("number");
            Property(o => o.State).IsRequired().HasColumnName("state");
            Property(o => o.Description).IsRequired().HasColumnName("description");

            HasOptional(o => o.CameraSettings).WithRequired(o=>o.Camera);

            ToTable("cameras");
        }
    }
}
