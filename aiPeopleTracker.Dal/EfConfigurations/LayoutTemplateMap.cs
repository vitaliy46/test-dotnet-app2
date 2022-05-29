using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.EfConfigurations
{
    public class LayoutTemplateMap : BaseEntityMap<LayoutTemplateDto, int>
    {
        public LayoutTemplateMap()
        {
            Property(o => o.Name).IsRequired().HasColumnName("name");
            Property(o => o.ItemsX).IsRequired().HasColumnName("items_x");
            Property(o => o.ItemsY).IsRequired().HasColumnName("items_y");
            Property(o => o.IsActive).IsRequired().HasColumnName("is_active");
            Property(o => o.UsageIndex).IsRequired().HasColumnName("usage_index");
     
            ToTable("layout_templates");
        }
    }
}
