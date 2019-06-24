using AgpWps.Model.ViewModels;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using DryIoc;
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

            // Services
            container.Register<IWpsClient, WpsClient>(Reuse.Singleton);

            // View Models
            container.Register<CapabilitiesViewModel>();

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
