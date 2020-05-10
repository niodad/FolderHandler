using Files;
using System;
using Xunit;

namespace XUnitTests
{
    public class DatePickerTests
    {
        [Theory]
        //[InlineData("./Resources/DSC_0258.JPG", 2015)]
        //[InlineData("./Resources/IMG_0123.JPG", null)]
        [InlineData("./Resources/CFNV9004.MOV", 2016)]

        public void File_GetDateTakenFromImage_DateTime(string path, int? year)
        {
            //Arrange

            //act
            var datePicker = DatePickerFactory.Get(path);

            //assert

            Assert.Equal(year, datePicker.DateTaken?.Year);
        }
    }
}
