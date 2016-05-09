namespace ChocoPacker.Burn
{
    internal static class Constants
    {
        public const string InstallerTypeName = "BurnBootstrapper";
        
        public const string BurnNamespace = "http://schemas.microsoft.com/wix/2008/Burn";
        
        public const int MegabyteSize = 1024 * 1024;
        
        // Burn use short names like 0, u0, u1, etc.
        public const int MaxRelativePathLength = 5;
        
        public const string InstallArguments = "/install /quiet /norestart";
        
        public const string UninstallArguments = "/uninstall /quiet /norestart";
        
        public const string BurnPackagesRoot = @"%ALLUSERSPROFILE%\Package Cache\";
    }
}