using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.EfConfigurations
{
    public class FloorPlanMap : BaseEntityMap<FloorPlanDto, int>
    {
        public FloorPlanMap()
        {
            Property(o => o.Description).IsRequired().HasColumnName("description");
            Property(o => o.Uri).IsRequired().HasColumnName("uri");
            Property(o => o.UriWithCameras).IsRequired().HasColumnName("uri_with_cameras");

            //HasRequired(o => o.TypeReference).WithMany().Map(o => o.MapKey("cda_document_type_id"));
            //HasRequired(o => o.MedicalRecord).WithMany(o => o.CdaDocuments).Map(o => o.MapKey("medical_record_id"));

            ToTable("floor_plans");
        }
    }
}
