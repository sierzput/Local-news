using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace LocalNews.Tests.Helpers
{
    public class TestsBase
    {
        protected readonly IFixture Fixture;

        protected TestsBase()
        {
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }
    }
}