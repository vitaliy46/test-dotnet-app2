using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.EfConfigurations
{
    public abstract class BaseEntityMap<TDto, TKey> : EntityTypeConfiguration<TDto>
        where TDto : DtoBase<TKey>
        where TKey : struct
    {
        protected BaseEntityMap()
        {
            //https://stackoverflow.com/questions/25094711/entity-framework-auto-generate-guid
            Property(u => u.Id).HasColumnName("id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(u => u.CreateDate).HasColumnName("create_date");
            Property(u => u.UpdateDate).HasColumnName("update_date");
        }
    }
}
