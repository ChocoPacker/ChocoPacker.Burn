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
                    
               var manifest = archive.ExtractBurnManifest(manifestEntry);
               return manifest.ParseInstallerInfo();
            });
    }
}