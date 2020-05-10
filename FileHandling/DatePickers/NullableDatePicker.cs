using System;
using System.Collections.Generic;
using System.Text;

namespace Files
{
    class NullableDatePicker : IDatePicker
    {
        public DateTime? DateTaken => null;
    }
}
