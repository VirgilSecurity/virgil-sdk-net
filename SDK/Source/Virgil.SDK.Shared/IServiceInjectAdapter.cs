namespace Virgil.SDK
{
    using System;

    /// <summary>
    /// This interface represents a service resolver which can substitute the services.
    /// </summary>
    public interface IServiceInjectAdapter
    {
        /// <summary>
        /// Determines whether the specified type can be resolved.
        /// </summary>
        bool CanResolve(Type serviceType);

        /// <summary>
        /// Resolves an instamc
        /// </summary>
        /// <returns>The instance of resolved service</returns>
        object Resolve(Type serviceType);
    }
}