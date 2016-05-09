using ChocoPacker.Common;
using Xunit;

namespace ChocoPacker.Burn.Tests
{
    public class BurnInstallDetectorTests
    {
        private readonly string _sevenZipPath;
        
        public BurnInstallDetectorTests()
        {
            _sevenZipPath = TestUtils.GetTestFilePath("7z.exe");
        }
        
        [Fact]
        public void GetInstallerTypeName_Returns_Proper_Name()
        {
            var installDetector = new BurnInstallDetector(new GeneralConfiguration
            {
                SevenZipPath = _sevenZipPath
            });
            
            var name = installDetector.GetInstallerTypeName(TestUtils.GetTestFilePath("dotnet-dev-win-x64.latest.exe"));
            Assert.Equal("BurnBootstrapper", name);
        }
        
        [Fact]
        public void GetInstallerTypeName_Throws_Exception_For_Bad_Installer()
        {
            var installDetector = new BurnInstallDetector(new GeneralConfiguration
            {
                SevenZipPath = _sevenZipPath
            });
            
            Assert.Throws<WrongInstallerTypeException>(() => installDetector.GetInstallerTypeName(_sevenZipPath));
        }
    }
}