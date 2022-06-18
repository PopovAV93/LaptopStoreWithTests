using LaptopStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LaptopStore.IntegrationTests
{
    public class TestHelper
    {
        public AppDBContent GetAppDBContent()
        {
            var contextOptions = new DbContextOptionsBuilder<AppDBContent>().UseInMemoryDatabase("db_test")
                        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;

            var context = new AppDBContent(contextOptions);

            return context;
        }
    }
}
