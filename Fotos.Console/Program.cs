using Files;
using System;
using System.Threading.Tasks;

namespace Fotos.Console
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            try
            {
                var file = new Folder();
                var copyFilesParameters = new CopyFilesParameters { 
                  extension ="",
                   searchOption = System.IO.SearchOption.TopDirectoryOnly,
                   sourceFolder =@"",
                   targetFolder =@""
                   
                };
                var s = await file.CopyFiles(copyFilesParameters,WriteCounterToConsole);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);

            }

        }

        private static void WriteCounterToConsole(string info)
        {
            System.Console.WriteLine(info);
          
        }
    }
}
