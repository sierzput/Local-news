using System;
using Ninject.Extensions.Factory;
using Ninject.Modules;

namespace LocalNews.Ioc
{
    public abstract class BaseIocModule : NinjectModule
    {
        protected void RegisterFactory<TFactory>() where TFactory : class
        {
            Bind<TFactory>().ToFactory();
        }

        protected void RegisterSmartFactory<TFactory>() where TFactory : class
        {
            var instanceProvider = new ConcreteTypesFactoryInstanceProvider<TFactory>();
            Bind<TFactory>().ToFactory(() => instanceProvider);
        }

        protected void RegisterTransient<TImplementation>() where TImplementation : class
        {
            Bind<TImplementation>().ToSelf().InTransientScope();
        }

        protected void RegisterTransient<TInterface>(Func<TInterface> factory) where TInterface : class
        {
            Bind<TInterface>().ToMethod(c => factory()).InTransientScope();
        }

        protected void RegisterTransient<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Bind<TInterface>().To<TImplementation>().InTransientScope();
        }

        protected void RegisterSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Bind<TInterface>().To<TImplementation>().InSingletonScope();
        }

        protected void RebindSingleton<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            Rebind<TInterface>().To<TImplementation>().InSingletonScope();
        }

        protected void RegisterSingleton<TImplementation>() where TImplementation : class
        {
            RegisterSingleton<TImplementation, TImplementation>();
        }

        protected void RegisterSingleton<TInterface>(Func<TInterface> factory) where TInterface : class
        {
            Bind<TInterface>().ToMethod(c => factory()).InSingletonScope();
        }

        protected void RegisterSingleton<TInterfaceA, TInterfaceB, TImplementation>()
            where TInterfaceA : class
            where TInterfaceB : class
            where TImplementation : class, TInterfaceA, TInterfaceB
        {
            Bind<TInterfaceA, TInterfaceB>()
                .To<TImplementation>()
                .InSingletonScope();
        }
    }
}
