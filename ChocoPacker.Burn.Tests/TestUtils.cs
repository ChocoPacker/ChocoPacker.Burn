using Microsoft.DotNet.PlatformAbstractions;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace ChocoPacker.Burn.Tests
{
    internal static class TestUtils
    {
        public static Stream ReadResource(string resourcePath)
        {
            var assembly = typeof(TestUtils).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(resourcePath);
        }
        
        public static XDocument ReadXmlResource(string resourcePath)
        {
            using (var stream = ReadResource(resourcePath))
            {
                return XDocument.Load(stream);
            }
        }

        public static string GetTestFilePath(string name)
            => Path.Combine(ApplicationEnvironment.ApplicationBasePath, "TestFiles", name);
    }
}
