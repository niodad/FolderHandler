using System;
using System.Collections.Generic;
using System.Text;

namespace Files
{

    public static class DatePickerFactory
    {
        /// <summary>
        /// Decides which class to instantiate.
        /// </summary>
        public static IDatePicker Get(string path)
        {
            var extension = System.IO.Path.GetExtension(path);
            switch (extension.ToUpper())
            {
                case ".JPG":
                    return new ImageDatePicker(path);
                case ".MOV":
                    return new MovieDatePicker(path);

                default:
                    return new MovieDatePicker(path);
            }
        }
    }
}
