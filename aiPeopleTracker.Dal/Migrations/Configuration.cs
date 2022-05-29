using aiPeopleTracker.Dal.Api.Dto;

namespace aiPeopleTracker.Dal.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<ATTrade.Data.AiPeopleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        /// This method will be called after migrating to the latest version.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ATTrade.Data.AiPeopleContext context)
        {
           //TODO включить после настройки выгрузаемых данных
           // AddTestDataToDb(context); 
        }
        /// <summary>
        /// You can use the DbSet<T>.AddOrUpdate() helper extension method 
        //  to avoid creating duplicate seed data.
        /// </summary>
        /// <param name="context"></param>
        private void AddTestDataToDb(ATTrade.Data.AiPeopleContext context)
        {

            context.Set<PersonDto>().AddOrUpdate(
                new PersonDto(),
                new PersonDto()
                );

            context.Set<CameraDto>().AddOrUpdate(
                new CameraDto(),
                new CameraDto()
            );

            context.Set<LayoutTemplateDto>().AddOrUpdate(
                new LayoutTemplateDto(),
                new LayoutTemplateDto()
            );
            
            context.Set<FloorPlanDto>().AddOrUpdate(
                new FloorPlanDto(),
                new FloorPlanDto()
            );
        }
    }
}
