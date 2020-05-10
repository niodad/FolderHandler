using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Files
{
    public class ImageDatePicker : IDatePicker
    {
        //we init this once so that if the function is repeatedly called
        //it isn't stressing the garbage man
        private static readonly Regex regex = new Regex(":");
        private readonly string FilePath;
        public ImageDatePicker(string path)
        {
            FilePath = path;
        }

        public DateTime? DateTaken { get 
            
            {
                try
                {
                    using (var fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                    using (var myImage = Image.FromStream(fileStream, false, false))
                    {
                        var propertyItem = myImage.GetPropertyItem(36867);
                        string dateTaken = regex.Replace(Encoding.UTF8.GetString(propertyItem.Value), "-", 2);
                        return DateTime.Parse(dateTaken);
                    }
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }

    }
}
