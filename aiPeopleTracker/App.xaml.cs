using System.Configuration;
using System.Data.Entity.Migrations;
using System.Windows;
using aiPeopleTracker.Startup;
using aiPeopleTracker.TestData;

namespace aiPeopleTracker
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ///Автоматическое накатывание новых миграций
            var config = new aiPeopleTracker.Dal.Migrations.Configuration();
            var migrator = new DbMigrator(config);
            migrator.Update();

            var mapperConfig = MapperConfig.Create();

            var container = UnityConfig.Create(mapperConfig);

            TestDataLoader.Load(container);

            var window = new MainWindow(container);
            
            window.Show();
        }
    }
}
