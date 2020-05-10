

using System;
using System.Data;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Files
{
    public class Folder : IFolder
    {
        /// <summary>
        /// Copy files with certain type from one dir to another dir
        /// </summary>
        /// <param name="dirFrom"></param>
        /// <param name="targetDirectory"></param>
        /// <param name="searchPattern"></param>
        /// 
        public delegate void GetInfoFromFunction(string info);
        public async Task<bool> CopyFiles(CopyFilesParameters copyFilesParameters, GetInfoFromFunction getInfo)
        {

            Validate(copyFilesParameters, getInfo);
            var files = Directory.GetFiles(copyFilesParameters.sourceFolder, copyFilesParameters.extension, copyFilesParameters.searchOption.Value);
            var filesCount = files.Count();
            getInfo($"{filesCount} Files to copy");
            getInfo($"");
             Parallel.ForEach(files, (file) =>
            {
                CopyFile(copyFilesParameters, file, getInfo);
            
            });

            getInfo($"{filesCount} Files copied");

            return await Task.FromResult(true);
        }

        private static void CopyFile(CopyFilesParameters copyFilesParameters, string file, GetInfoFromFunction getInfo)
        {
            var DatePicker = DatePickerFactory.Get(file);
            var newFile = file.Replace(copyFilesParameters.sourceFolder, copyFilesParameters.targetFolder);
            var targetDir = Path.GetDirectoryName(newFile);
            targetDir = Path.Combine(targetDir, DatePicker.DateTaken?.Year.ToString() ?? string.Empty);
            var fileName = Path.GetFileName(newFile);
            Directory.CreateDirectory(targetDir);
            fileName = Path.Combine(targetDir, fileName);
            File.Copy(file, fileName, true);
            getInfo($"{fileName}");
        }

        private static void Validate(CopyFilesParameters copyFilesParameters, GetInfoFromFunction getInfo)
        {
            if (!Directory.Exists(copyFilesParameters.sourceFolder))
            {
                var message = $"Directory '{copyFilesParameters.sourceFolder}' does not exist";
                throw new ArgumentException(message, nameof(copyFilesParameters.sourceFolder));
            }
            try
            {
                Directory.CreateDirectory(copyFilesParameters.targetFolder);
            }
            catch (Exception ex)
            {
                var message = $"Could not create directory '{copyFilesParameters.targetFolder}'";
                throw new ArgumentException(message, nameof(copyFilesParameters.targetFolder), ex);
            } 
        }



    }
}
