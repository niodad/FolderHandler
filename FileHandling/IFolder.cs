using System.Threading.Tasks;

namespace Files
{
    public interface IFolder
    {
        Task<bool> CopyFiles(CopyFilesParameters copyFilesParameters, Folder.GetInfoFromFunction getInfo);
    }
}