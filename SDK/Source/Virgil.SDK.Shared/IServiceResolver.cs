namespace Virgil.SDK
{
    /// <summary>
    /// This interface represents a servoce resolver which can substitute the services.
    /// </summary>
    public interface IServiceResolver
    {
        /// <summary>
        /// Determines whether the specified type can be resolved .
        /// </summary>
        bool CanResolve<TService>();

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <returns></returns>
        TService Resolve<TService>();
    }
}