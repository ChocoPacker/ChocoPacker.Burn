using ChocoPacker.Common;
using Xunit;

namespace ChocoPacker.Burn.Tests
{
    public class BurnInstallerInfoProviderTests
    {
        private readonly string _sevenZipPath;
        
        public BurnInstallerInfoProviderTests()
        {
            _sevenZipPath = TestUtils.GetTestFilePath("7z.exe");
        }
        
        [Fact]
        public void GetInstallerInfo_Proper_Installer_Success()
        {
            const string expectedUninstall = "%ALLUSERSPROFILE%\\Package Cache\\{aa4ffaa7-f2a1-40c4-a7b9-e2424e3620f8}\\dotnet-dev-win-x64.1.0.0-rc2-002543.exe"; 
            var provider = new BurnInstallerInfoProvider(new GeneralConfiguration
            {
                SevenZipPath = _sevenZipPath
            });
            
            var info = provider.GetInstallerInfo(TestUtils.GetTestFilePath("dotnet-dev-win-x64.latest.exe"));
            Assert.Equal("Microsoft Corporation", info.Author);
            Assert.Equal("dotnet-dev-win-x64.latest.exe", info.InstallExecutable);
            Assert.Equal("/install /quiet /norestart", info.InstallArguments);
            Assert.Equal("Microsoft .NET Core CLI for Windows (1.0.0-rc2-002543)", info.ProductName);
            Assert.Equal("1.0.0.2543", info.ProductVersion);
            Assert.Equal(expectedUninstall, info.UninstallExecutable);
            Assert.Equal("/install /quiet /norestart", info.UninstallArguments);            
        }
        
        [Fact]
        public void GetInstallerInfo_Bad_Installer_WrongInstallerTypeException()
        {
            var provider = new BurnInstallerInfoProvider(new GeneralConfiguration
            {
                SevenZipPath = _sevenZipPath
            });
            
            Assert.Throws<WrongInstallerTypeException>(() => provider.GetInstallerInfo(_sevenZipPath));
        }
    }
}