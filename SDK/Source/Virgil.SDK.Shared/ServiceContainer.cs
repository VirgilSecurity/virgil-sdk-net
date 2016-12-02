#region Copyright (C) Virgil Security Inc.
// Copyright (C) 2015-2016 Virgil Security Inc.
// 
// Lead Maintainer: Virgil Security Inc. <support@virgilsecurity.com>
// 
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions 
// are met:
// 
//   (1) Redistributions of source code must retain the above copyright
//   notice, this list of conditions and the following disclaimer.
//   
//   (2) Redistributions in binary form must reproduce the above copyright
//   notice, this list of conditions and the following disclaimer in
//   the documentation and/or other materials provided with the
//   distribution.
//   
//   (3) Neither the name of the copyright holder nor the names of its
//   contributors may be used to endorse or promote products derived 
//   from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ''AS IS'' AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT,
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
// HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING
// IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
#endregion

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

        public void RegisterInstance<TResolve>(object concrete) 
        {
            this.Register(new RegisteredObject(typeof(TResolve), concrete.GetType(), false)
            {
                Instance = concrete
            });
        }

        public TResolvingType Resolve<TResolvingType>()
        {
            return (TResolvingType)this.ResolveObject(typeof(TResolvingType));
        }

        public void RemoveService<TResolve>()
        {
            var registeredObject = this.registeredObjects
                .SingleOrDefault(it => it.ResolvingType == typeof(TResolve));

            if (registeredObject != null)
            {
                this.registeredObjects.Remove(registeredObject);
            }
        }

        public void Clear()
        {
            this.registeredObjects.Clear();
        }
        
        private void Register(RegisteredObject registeredObject)
        {
            if (this.registeredObjects.Any(it => it.ResolvingType == registeredObject.ResolvingType))
            {
                throw new ServiceIsAlreadyRegistered();
            }

            this.registeredObjects.Add(registeredObject);
        }

        private object ResolveObject(Type typeToResolve)
        {
            var registeredObject = this.registeredObjects.FirstOrDefault(o => o.ResolvingType == typeToResolve);
            if (registeredObject == null)
            {
                throw new ServiceNotRegisteredException($"The type {typeToResolve.Name} has not been registered");
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