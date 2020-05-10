using Brain2CPU.ExifTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Files
{

    public class MovieDatePicker : IDatePicker
    {
        private readonly string FilePath;
        public MovieDatePicker(string path)
        {
            FilePath = path;
        }
        public DateTime? DateTaken
        {
            get
            {
                try
                {
                    var fileIfno = new FileInfo(FilePath);
                    using (var etw = new ExifToolWrapper(@"./Resources/exiftool(-k).exe"))
                    {
                        etw.Start();
                        var d = etw.FetchExifFrom(FilePath);
                        var s = d.Where(c => new List<string> { "Creation Date", "Create Date" }.Contains(c.Key)  ).Select(c=>c.Value).FirstOrDefault();

                        return Convert.ToDateTime(s);
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