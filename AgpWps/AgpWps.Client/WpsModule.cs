using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;

namespace AgpWps.Client
{
    internal class WpsModule : Module
    {
        private static WpsModule _instance;

        /// <summary>
        /// Retrieve the singleton instance to this module here
        /// </summary>
        public static WpsModule Current => _instance ?? (_instance = (WpsModule)FrameworkApplication.FindModule("AgpWps.Client_Module"));

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
