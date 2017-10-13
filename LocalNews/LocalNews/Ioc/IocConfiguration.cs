using Ninject;
using XLabs.Ioc;
using XLabs.Ioc.Ninject;

namespace LocalNews.Ioc
{
    public static class IocConfiguration
    {
        public static void Configure()
        {
            var standardKernel = new StandardKernel();
            var resolverContainer = new NinjectContainer(standardKernel);

            standardKernel.Load(new IocModule());

            Resolver.ResetResolver();
            Resolver.SetResolver(resolverContainer.GetResolver());
        }
    }
}