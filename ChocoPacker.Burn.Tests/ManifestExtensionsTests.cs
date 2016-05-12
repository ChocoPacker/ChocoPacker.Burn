using Xunit;

namespace ChocoPacker.Burn.Tests
{
    public class ManifestExtensionsTests
    {
        [Fact]
        public void ParseInstallerInfo_Parse_Proper_Manifest()
        {
            const string expectedUninstall = "\"%ALLUSERSPROFILE%\\Package Cache\\{aa4ffaa7-f2a1-40c4-a7b9-e2424e3620f8}\\dotnet-dev-win-x64.1.0.0-rc2-002543.exe\" /uninstall /quiet /norestart"; 
            var resource = TestUtils.ReadXmlResource("ChocoPacker.Burn.Tests.TestData.PrettyManifest.xml");
            var installerInfo = resource.ParseInstallerInfo();
            
            Assert.Equal("Microsoft Corporation", installerInfo.Author);
            Assert.Equal("Microsoft .NET Core CLI for Windows (1.0.0-rc2-002543)", installerInfo.ProductName);
            Assert.Equal("1.0.0.2543", installerInfo.ProductVersion);
            Assert.Equal(expectedUninstall, installerInfo.UninstallString);
        }
    }
}