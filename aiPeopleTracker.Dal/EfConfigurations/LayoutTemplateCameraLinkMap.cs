using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.EfConfigurations
{
    public class LayoutTemplateCameraLinkMap : BaseEntityMap<LayoutTemplateCameraLinkDto, int>
    {
        public LayoutTemplateCameraLinkMap()
        {
            Property(o => o.X).IsRequired().HasColumnName("x");
            Property(o => o.Y).IsRequired().HasColumnName("y");
            Property(o => o.LayoutTemplateId).IsRequired().HasColumnName("layout_template_id");

            Property(o => o.CameraId).IsRequired().HasColumnName("camera_id");

            ToTable("layout_template_camera_links");
        }
    }
}
