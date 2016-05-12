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
                    .First(x => archive.IsBurnManifest(x));
               
               var fileName = Path.GetFileName(installerPath);  
               var manifest = archive.ExtractBurnManifest(manifestEntry);
               var info = manifest.ParseInstallerInfo();
               info.InstallString = $"\"{fileName}\" {InstallArguments}";
               return info;
            });
    }
}