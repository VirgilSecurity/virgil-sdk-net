namespace Virgil.SDK
{
    /// <summary>
    /// This interface represents a service resolver which can substitute the services.
    /// </summary>
    public interface IServiceInjectAdapter
    {
        /// <summary>
        /// Determines whether the specified type can be resolved.
        /// </summary>
        bool CanResolve<TService>();

        /// <summary>
        /// Resolves an instamc
        /// </summary>
        /// <returns>The instance of resolved service</returns>
        TService Resolve<TService>();
    }
}