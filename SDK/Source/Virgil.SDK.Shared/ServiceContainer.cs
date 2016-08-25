namespace Virgil.SDK
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Virgil.SDK.Exceptions;

    internal class ServiceContainer
    {
        private readonly IList<RegisteredObject> registeredObjects = new List<RegisteredObject>();
        
        public void RegisterSingleton<TResolve, TConcrete>() where TConcrete : TResolve
        {
            this.Register(new RegisteredObject(typeof(TResolve), typeof(TConcrete), false));
        }

        public void RegisterTransient<TResolve, TConcrete>() where TConcrete : TResolve
        {
            this.Register(new RegisteredObject(typeof(TResolve), typeof(TConcrete), true));
        }

        public void RegisterInstance<TResolve, TConcrete>(TConcrete concrete) where TConcrete : TResolve
        {
            this.Register(new RegisteredObject(typeof(TResolve), typeof(TConcrete), false)
            {
                Instance = concrete
            });
        }

        public TResolvingType Resolve<TResolvingType>()
        {
            return (TResolvingType)this.ResolveObject(typeof(TResolvingType));
        }

        public void Clear()
        {
            this.registeredObjects.Clear();
        }
        
        private void Register(RegisteredObject registeredObject)
        {
            if (this.registeredObjects.Any(it => it.ResolvingType == registeredObject.ResolvingType))
            {
                throw new TypeIsAlreadyRegistered();
            }

            this.registeredObjects.Add(registeredObject);
        }

        private object ResolveObject(Type typeToResolve)
        {
            var registeredObject = this.registeredObjects.FirstOrDefault(o => o.ResolvingType == typeToResolve);
            if (registeredObject == null)
            {
                throw new TypeNotRegisteredException($"The type {typeToResolve.Name} has not been registered");
            }

            return this.GetInstance(registeredObject);
        }

        private object GetInstance(RegisteredObject registeredObject)
        {
            if (registeredObject.Instance != null && !registeredObject.IsTransient)
            {
                return registeredObject.Instance;
            }

            var parameters = this.ResolveConstructorParameters(registeredObject);
            var instance = Activator.CreateInstance(registeredObject.ConcreteType, parameters.ToArray());

            registeredObject.Instance = instance;
            return instance;
        }

        private IEnumerable<object> ResolveConstructorParameters(RegisteredObject registeredObject)
        {
            var constructorInfo = registeredObject.ConcreteType.GetConstructors().First();
            return constructorInfo.GetParameters().Select(parameter => this.ResolveObject(parameter.ParameterType));
        }

        private class RegisteredObject
        {
            public RegisteredObject(Type resolvingType, Type concreteType, bool isTransient)
            {
                this.ResolvingType = resolvingType;
                this.ConcreteType = concreteType;
                this.IsTransient = isTransient;
            }

            public bool IsTransient { get; }

            public object Instance { get; set; }

            public Type ResolvingType { get; }

            public Type ConcreteType { get; }
        }
    }
}