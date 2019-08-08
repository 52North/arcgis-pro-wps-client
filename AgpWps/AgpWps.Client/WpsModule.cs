using System.IO;
using AgpWps.Client.Services;
using AgpWps.Model.Factories;
using AgpWps.Model.Services;
using AgpWps.Model.ViewModels;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using DryIoc;
using System.Net.Http;
using AgpWps.Model.Repositories;
using Wps.Client.Services;

namespace AgpWps.Client
{
    internal class WpsModule : Module
    {

        private static WpsModule _instance;

        public IContainer Container { get; }

        public WpsModule()
        {
            Container = CreateContainer();
        }

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static WpsModule Current => _instance ?? (_instance = (WpsModule)FrameworkApplication.FindModule("AgpWps.Client_Module"));

        private static IContainer CreateContainer()
        {
            var container = new Container();

            var client = new WpsClient(new HttpClient(), new XmlSerializationService());

            // Services
            container.RegisterInstance<IWpsClient>(client);
            container.Register<IFileReader, FileReader>();
            container.Register<IRequestFactory, RequestFactory>();
            container.Register<IContext, ArcgisContext>();
            container.Register<IDialogService, DialogService>();
            container.Register<IViewModelFactory, ViewModelFactory>();
            container.Register<IMapService, MapService>(Reuse.Singleton);
            container.Register<IAppData, AppData>();

            // Repositories
            container.Register<IServerRepository, ServerRepository>(setup: Setup.With(trackDisposableTransient: true));
            container.Register<IResultRepository, ResultsRepository>(setup: Setup.With(trackDisposableTransient: true));

            // View Models
            container.Register<AddServerPopupViewModel>();
            container.Register<CapabilitiesViewModel>();
            container.Register<ResultsViewModel>();

            /*
             * The ResultsViewModel must be initialized before the used does it when opening the results panel.
             * Not doing so will end up in an empty results panel even after executing a couple of processes.
             */
            var resultsVm = new ResultsViewModel(container.Resolve<IContext>(), container.Resolve<IResultRepository>());
            container.RegisterInstance(resultsVm);

            return container;
        }

        #region Overrides
        /// <summary>
        /// Called by Framework when ArcGIS Pro is closing
        /// </summary>
        /// <returns>False to prevent Pro from closing, otherwise True</returns>
        protected override bool CanUnload()
        {
            //TODO - add your business logic
            //return false to ~cancel~ Application close
            return true;
        }

        #endregion Overrides

    }
}
