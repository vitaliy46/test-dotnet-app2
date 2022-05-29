using System;
using System.Diagnostics;
using aiPeopleTracker.Dal.Api.Dto;
using aiPeopleTracker.Dal.EfConfigurations;

namespace ATTrade.Data
{
    using System.Data.Entity;

    /// <summary>
    /// Класс взаимодействия служб Entity Framework с кодом описывающим модель бизнесс данных.
    /// </summary>
    public class AiPeopleContext : DbContext
    {

        public AiPeopleContext() : base("Connection"+ Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.User))
        {
            Debug.WriteLine($"Строка подключения: Connection{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.User)}" );
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CameraMap());
            modelBuilder.Configurations.Add(new CameraSettingsMap());
            modelBuilder.Configurations.Add(new FloorPlanMap());
            modelBuilder.Configurations.Add(new LayoutTemplateCameraLinkMap());
            modelBuilder.Configurations.Add(new LayoutTemplateMap());
            modelBuilder.Configurations.Add(new PersonMap());
            //modelBuilder.Configurations.Add(new PersonPersonTagLinkMap());
            modelBuilder.Configurations.Add(new PersonTagMap());

        }

        public virtual DbSet<TDto> GetSet<TDto, TKey>() 
            where TDto : DtoBase<TKey>
        {
            return this.Set<TDto>();
        }
        
        // Ролевая модель.
        //public DbSet<User> Users { get; set; }
        

    }

}
