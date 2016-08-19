namespace Virgil.SDK.Tests
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;

    public class ServiceContainerTests
    {
        [Test]
        public void RegisterSingleton_ResolvingTypeAndConcreteType_ShouldResolveSingleInstance()
        {
            var ioc = new ServiceContainer();

            ioc.RegisterSingleton<IIocTest1, IocTest1>();
            var resolvedType = ioc.Resolve<IIocTest1>();

            resolvedType.Should().NotBeNull();
            resolvedType.Should().BeOfType<IocTest1>();

            resolvedType.Should().Be(ioc.Resolve<IIocTest1>());
        }

        [Test]
        public void RegisterTransient_ResolvingTypeAndConcreteType_ShouldResolveTransientInstance()
        {
            var ioc = new ServiceContainer();

            ioc.RegisterTransient<IIocTest1, IocTest1>();
            var resolvedType = ioc.Resolve<IIocTest1>();

            resolvedType.Should().NotBeNull();
            resolvedType.Should().BeOfType<IocTest1>();

            resolvedType.Should().NotBe(ioc.Resolve<IIocTest1>());
        }

        [Test]
        public void RegisterInstance_ResolvingTypeAndInstanceAsParameter_ShouldResolvePassedInstance()
        {
            var ioc = new ServiceContainer();

            var instance = new IocTest1();

            ioc.RegisterInstance<IIocTest1, IocTest1>(instance);
            var resolvedType = ioc.Resolve<IIocTest1>();

            resolvedType.Should().NotBeNull();
            resolvedType.Should().BeOfType<IocTest1>();

            resolvedType.Should().Be(instance);
        }

        [Test]
        public void Resolve_ResolvingType_ShouldResolveTypeWithMultipleDependencies()
        {
            var ioc = new ServiceContainer();

            var instance = new IocTest1();

            ioc.RegisterInstance<IIocTest1, IocTest1>(instance);
            ioc.RegisterSingleton<IIocTest3, IocTest3>();
            ioc.RegisterTransient<IIocTest2, IocTest2>();

            var resolvedType = ioc.Resolve<IIocTest2>();

            resolvedType.Should().NotBeNull();
            resolvedType.Should().BeOfType<IocTest2>();

            resolvedType.Test1.Should().Be(ioc.Resolve<IIocTest1>());
            resolvedType.Test3.Should().Be(ioc.Resolve<IIocTest3>());
        }

        [Test]
        public void Resolve_ResolvingType_ShouldResolveTypeUsingInjectedAdapter()
        {
            var ioc = new ServiceContainer();
            ioc.SetInjectAdapter(new IocInjectAdapterTest1(new IocTest1()));
            
            var resolvedType = ioc.Resolve<IIocTest1>();

            resolvedType.Should().NotBeNull();
            resolvedType.Should().BeOfType<IocTest1>();
        }

        [Test]
        public void Resolve_ResolvingType_ShouldResolveTypeUsingInjectedAdapterDependencies()
        {
            var ioc = new ServiceContainer();   
            var injectedTest1 = new IocTest1();
            ioc.SetInjectAdapter(new IocInjectAdapterTest1(injectedTest1));

            var iocTest3 = new IocTest3();

            ioc.RegisterTransient<IIocTest2, IocTest2>();
            ioc.RegisterInstance<IIocTest3, IocTest3>(iocTest3);

            var resolvedType = ioc.Resolve<IIocTest2>();

            resolvedType.Should().NotBeNull();
            resolvedType.Should().BeOfType<IocTest2>();

            resolvedType.Test1.Should().Be(injectedTest1);
            resolvedType.Test3.Should().Be(iocTest3);
        }

        public interface IIocTest1
        {
        }

        public interface IIocTest2
        {
            IIocTest1 Test1 { get; }
            IIocTest3 Test3 { get; }
        }

        public interface IIocTest3
        {
        }

        public class IocTest1 : IIocTest1
        {
        }

        public class IocTest2 : IIocTest2
        {
            public IIocTest1 Test1 { get; set; }
            public IIocTest3 Test3 { get; set; }

            public IocTest2(IIocTest1 test1, IIocTest3 test3)
            {
                this.Test1 = test1;
                this.Test3 = test3;
            }
        }

        public class IocTest3 : IIocTest3
        {
        }

        public class IocInjectAdapterTest1 : IServiceInjectAdapter
        {
            private readonly IocTest1 internalTest1;

            public IocInjectAdapterTest1(IocTest1 internalTest1)
            {
                this.internalTest1 = internalTest1;
            }

            public bool CanResolve(Type serviceType)
            {
                return serviceType == typeof(IIocTest1);
            }

            public object Resolve(Type serviceType)
            {
                return this.internalTest1;
            }
        }
    }
}