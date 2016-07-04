using System;
using ChocoPacker.Common;
using ChocoPacker.SevenZip;

namespace ChocoPacker.Burn
{
    public abstract class BurnBase
    {
        private readonly ISevenZipExtractor _sevenZipExtractor;

        protected BurnBase(IGeneralConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));    
            }
            
            if (string.IsNullOrEmpty(configuration.SevenZipPath))
            {
                throw new ArgumentException("Empty SevenZipPath provided");    
            }    
            
            _sevenZipExtractor = new SevenZipExtractor(configuration.SevenZipPath);
        }
        
        protected T ProcessBurnInstaller<T>(string installerPath, Func<IArchive, T> func)
        {
                        IArchive archive = null;
            try
            {
                archive = _sevenZipExtractor.OpenArchive(installerPath);
                return func(archive);
            }
            catch (WrongInstallerTypeException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new WrongInstallerTypeException($"File: '{installerPath}' isn't burn installer", ex);
            }
            finally
            {
                archive?.Dispose();
            }
        }
    }
}