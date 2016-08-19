namespace Virgil.SDK
{
    internal interface IServiceResolver
    {
        TService Resolve<TService>();
    }
}