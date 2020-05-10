using System.IO;

namespace Files
{
    public class CopyFilesParameters
    {
        public string sourceFolder { get; set; }
        public string targetFolder { get; set; }
        public string extension { get; set; }
        public SearchOption? searchOption { get; set; }

        public bool CanCopy()
        {
            var canCopy = true;

            if (string.IsNullOrEmpty(sourceFolder))
            {
                return false;
            }
            if (string.IsNullOrEmpty(targetFolder))
            {
                return false;
            }
            if (string.IsNullOrEmpty(extension))
            {
                return false;
            }

            if (searchOption is null)
            {
                searchOption = SearchOption.TopDirectoryOnly;
            }

            return canCopy;

        }
    }
}