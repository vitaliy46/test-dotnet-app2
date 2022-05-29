using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.EfConfigurations
{
    public class CameraSettingsMap : BaseEntityMap<CameraSettingsDto, int>
    {
        public CameraSettingsMap()
        {
            Property(o => o.SourceAddress).IsRequired().HasColumnName("source_address");
            Property(o => o.X).IsOptional().HasColumnName("x");
            Property(o => o.Y).IsOptional().HasColumnName("y");
            Property(o => o.IsActive).IsRequired().HasColumnName("is_active");

            //https://riptutorial.com/entity-framework/example/29154/mapping-one-to-zero-or-one
            Property(o => o.CameraId).IsRequired().HasColumnName("camera_id");
            HasRequired(o => o.Camera).WithOptional(o => o.CameraSettings);
            HasKey(o => o.CameraId);

            Property(o => o.FloorPlanId).IsOptional().HasColumnName("floor_plan_id");
            HasRequired(o => o.FloorPlan).WithMany().HasForeignKey(k => k.FloorPlanId);

            ToTable("camera_settings");
        }
    }
}
