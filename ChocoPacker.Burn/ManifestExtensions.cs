using System.IO;
using System.Xml.Linq;
using ChocoPacker.Common;
using static ChocoPacker.Burn.Constants;

namespace ChocoPacker.Burn
{
    internal static class ManifestExtensions
    {
        public static InstallerInfo ParseInstallerInfo(this XDocument doc)
        {
            var registration = doc.Root.Element(BurnName("Registration"));
            var arp = registration.Element(BurnName("Arp"));

            var guid = registration.ReadAttribute("Id");
            var exeName = registration.ReadAttribute("ExecutableName");
            var uninstallerPath = Path.Combine(BurnPackagesRoot, guid, exeName);
            
            return new InstallerInfo 
            {
                Author = arp.ReadAttribute("Publisher"),
                ProductName = arp.ReadAttribute("DisplayName"),
                ProductVersion = arp.ReadAttribute("DisplayVersion"),
                UninstallString = $"\"{uninstallerPath}\" {UninstallArguments}"
            };
        }
        
        private static XName BurnName(string name)
            => XName.Get(name, BurnNamespace);
            
        private static string ReadAttribute(this XElement elem, string name)
            => elem?.Attribute(name)?.Value;
    }
}