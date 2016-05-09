using System.Linq;
using ChocoPacker.Common;

namespace ChocoPacker.Burn
{
    public sealed class BurnInstallDetector : BurnBase, IInstallTypeDetector
    {
        public BurnInstallDetector(IGeneralConfiguration configuration) 
            : base(configuration)
        {
        }

        public string GetInstallerTypeName(string installerPath)
            => ProcessBurnInstaller(installerPath, archive => {
                var isBurn = archive.FilterNonBurnFiles().Any(x => archive.IsBurnManifest(x));
                if (isBurn)
                {
                    return Constants.InstallerTypeName;
                }
                
                throw new WrongInstallerTypeException($"File: '{installerPath}' isn't burn installer");
            });
    }
}