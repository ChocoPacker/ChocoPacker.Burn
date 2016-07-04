using System.IO;
using System.Linq;
using ChocoPacker.Common;
using static ChocoPacker.Burn.Constants;

namespace ChocoPacker.Burn
{
    [SupportsInstaller(InstallerTypeName)]
    public sealed class BurnInstallerInfoProvider : BurnBase, IInstallerInfoProvider
    {
        public BurnInstallerInfoProvider(IGeneralConfiguration configuration) 
            : base(configuration)
        {
        }

        public InstallerInfo GetInstallerInfo(string installerPath)
        =>  ProcessBurnInstaller(installerPath, archive => {
               var manifestEntry = archive
                    .FilterNonBurnFiles()
                    .First(archive.IsBurnManifest);
               
               var fileName = Path.GetFileName(installerPath);  
               var manifest = archive.ExtractBurnManifest(manifestEntry);
               var info = manifest.ParseInstallerInfo();
               info.InstallExecutable = fileName;
               info.InstallArguments = InstallArguments;
               return info;
            });
    }
}