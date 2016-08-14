namespace Virgil.SDK
{
    using System;

    internal class ServiceLocator
    {
        internal void SetServiceResolver(IServiceResolver resolver)
        {
            throw new NotImplementedException();
        }

        internal TService Resolve<TService>()
        {
            throw new NotImplementedException();
        }

        internal void Register<TService>(TService service)
        {
            throw new NotImplementedException();
        }

        internal void Clear()
        {
            throw new NotImplementedException();
        }
    }
}