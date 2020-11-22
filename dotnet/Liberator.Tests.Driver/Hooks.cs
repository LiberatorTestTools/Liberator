using Liberator.Driver;
using NUnit.Framework;

namespace Liberator.Tests.Driver
{
    [TestFixture]
    public static class Hooks
    {
        public static IRodent ratDriver;

        public static string WebsiteToTest { get; set; } = "http://localhost:80";
        public static string WebsiteUrl { get; set; } = "localhost";

        [TearDown]
        public static void TearDown()
        {
            if (ratDriver != null)
            {
                if (ratDriver.DriverName != null)
                {
                    ratDriver.ReturnEncapsulatedDriver().Close();
                }
            }
        }
    }
}
