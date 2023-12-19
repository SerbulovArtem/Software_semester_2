using ActionManager.BLL.Repositories.Concreate.DataBaseMCSQLActionManager;
using ActionManager.DAL.Data;
using ActionManager.DAL.Repositories.Abstract.DataBaseMCSQLActionManager;
using ActionManager.DAL.Repositories.Concreate.DataBaseMCSQLActionManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using WPF.ViewModels;
using WPF.Views;

namespace ActionManagerWPF
{
    public partial class App : Application
    {
        public IUnityContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            base.OnStartup(e);
            RegisterUnity();
            LoginView lf = Container.Resolve<LoginView>();
            bool? result = lf.ShowDialog();

            if (result.HasValue && result.Value)
            {
                ActionListView al = Container.Resolve<ActionListView>();
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                Current.MainWindow = al;
                al.Show();
            }
        }

        private void RegisterUnity()
        {
            Container = new UnityContainer();
            var configuration = new ConfigurationBuilder()
    .AddJsonFile("D:\\University\\Software_semester_2\\ActionManager.DAL\\Data\\appsettings.json", optional: false, reloadOnChange: true)
    .Build();

            var connectionString = configuration.GetConnectionString("BloggingDatabase");
            // Register DbContext
            Container.RegisterType<ActionManagerContext>(
               new HierarchicalLifetimeManager(),
               new InjectionConstructor(
                   new DbContextOptionsBuilder<ActionManagerContext>()
                       .UseSqlServer(connectionString)
            .Options
            )
           );


            // Register repositories
            Container.RegisterType<IActionsRepository, ActionManagerActionsRepository>(new HierarchicalLifetimeManager());
            Container.RegisterType<ITypeActionsRepository, ActionManagerTypeActionsRepository>(new HierarchicalLifetimeManager());
            Container.RegisterType<IProductsRepository, ActionManagerProductsRepository>(new HierarchicalLifetimeManager());

            // Register business logic services
            Container.RegisterType<IUsersRepository, ActionManagerUserRepository>(new HierarchicalLifetimeManager());

            // Register view models
            Container.RegisterType<ActionAddViewModel>(new HierarchicalLifetimeManager());
            Container.RegisterType<ActionChangeViewModel>(new HierarchicalLifetimeManager());
            Container.RegisterType<ActionListViewModel>(new HierarchicalLifetimeManager());
            Container.RegisterType<LoginViewModel>(new HierarchicalLifetimeManager());
            // Add other registrations as needed
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            Container?.Dispose();
        }
    }
}
