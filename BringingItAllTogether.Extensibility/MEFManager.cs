using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace BringingItAllTogether.Extensibility
{
    public class MEFManager
    {
        private static readonly ILog Log;

        static MEFManager()
        {
            Log = LogManager.GetLogger(typeof(MEFManager));
        }

        public static void Compose(object o)
        {
            try
            {
                var container = new CompositionContainer(new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Plugins")));
                var batch = new CompositionBatch();
                batch.AddPart(o);
                container.Compose(batch);
            }
            catch (FileLoadException loadEx)
            {
                // The Assembly has already been loaded.
                Log.Error(loadEx.Message, loadEx);
            }
            catch (BadImageFormatException imgEx)
            {
                // If a BadImageFormatException exception is thrown, the file is not an assembly.
                Log.Error(imgEx.Message, imgEx);
            }
            catch (ChangeRejectedException cex)
            {
                Log.Error(cex.Message, cex);
            }
            catch (ReflectionTypeLoadException ex)
            {
                var sb = new StringBuilder();
                foreach (var exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    var exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                var errorMessage = sb.ToString();
                Log.Error(errorMessage);
            }
            catch (Exception ex)
            {
                // General error trapping, although we might really want these to fail
                Log.Error(ex.Message, ex);
            }
        }

        public static void Compose(object o, string pathToExports)
        {
            try
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathToExports);
                var container = new CompositionContainer(new DirectoryCatalog(path));
                var batch = new CompositionBatch();
                batch.AddPart(o);
                container.Compose(batch);
            }
            catch (FileLoadException loadEx)
            {
                // The Assembly has already been loaded.
                Log.Error(loadEx.Message, loadEx);
            }
            catch (BadImageFormatException imgEx)
            {
                // If a BadImageFormatException exception is thrown, the file is not an assembly.
                Log.Error(imgEx.Message, imgEx);
            }
            catch (ChangeRejectedException cex)
            {
                Log.Error(cex.Message, cex);
            }
            catch (ReflectionTypeLoadException ex)
            {
                var sb = new StringBuilder();
                foreach (var exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    var exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                var errorMessage = sb.ToString();
                Log.Error(errorMessage);
            }
            catch (Exception ex)
            {
                // General error trapping, although we might really want these to fail
                Log.Error(ex.Message, ex);
            }
        }
    }
}
