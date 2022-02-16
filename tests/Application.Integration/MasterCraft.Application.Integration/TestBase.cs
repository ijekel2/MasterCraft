using NUnit.Framework;
using System.Threading.Tasks;
using static MasterCraft.Application.Integration.Testing;

namespace MasterCraft.Application.Integration
{
    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await Setup();
        }

        [TearDown]
        public async Task TestTearDown()
        {
            await TearDown();
        }
    }
}



