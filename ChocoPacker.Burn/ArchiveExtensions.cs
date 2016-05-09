using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ChocoPacker.SevenZip;
using static ChocoPacker.Burn.Constants;

namespace ChocoPacker.Burn
{
    internal static class ArchiveExtensions
    {
        public static bool IsBurnManifest(this IArchive archive, ICompressedFileInfo fileInfo)
        {
            try 
            {
                var doc = archive.ExtractBurnManifest(fileInfo);
                var elementToSearch = XName.Get("Registration", BurnNamespace);
                return doc.Root.Elements().Any(x => x.Name == elementToSearch);
            }
            catch
            {
                return false;
            }
        }
        
        public static XDocument ExtractBurnManifest(this IArchive archive, ICompressedFileInfo fileInfo)
        {
            using(var stream = archive.ExtractFile(fileInfo))
            {
                return XDocument.Load(stream);
            }
        }
        
        public static IEnumerable<ICompressedFileInfo> FilterNonBurnFiles(this IArchive archive)
            => archive.GetArchiveFiles()
                    .Where(x => x?.CompressedSize <= MegabyteSize || x?.Size <= MegabyteSize)
                    .Where(x => x.RelativePath.Length < MaxRelativePathLength);
    }
}