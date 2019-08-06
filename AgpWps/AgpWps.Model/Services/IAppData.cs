namespace AgpWps.Model.Services
{
    public interface IAppData
    {

        /// <summary>
        /// The path containing all the application configuration and data.
        /// </summary>
        string DataDirectory { get; }

    }
}
