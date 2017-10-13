using System.Linq;
using System.Reflection;
using Moq;
using Ploeh.AutoFixture;

namespace LocalNews.Tests.Helpers
{
    public static class AutoFixtureExtensions
    {
        public static T CreateWithFrozen<T>(this IFixture fixture)
        {
            var parameters = typeof(T).GetConstructors().Single().GetParameters();
            foreach (ParameterInfo parameter in parameters)
            {
                typeof(FixtureFreezer).GetMethod("Freeze", new[] {typeof(IFixture)})
                    .MakeGenericMethod(typeof(Mock<>)
                        .MakeGenericType(parameter.ParameterType))
                    .Invoke(null, new object[] {fixture});
            }

            return fixture.Create<T>();
        }

        public static Mock<T> Mock<T>(this IFixture fixture) where T : class
        {
            return fixture.Freeze<Mock<T>>();
        }
    }
}