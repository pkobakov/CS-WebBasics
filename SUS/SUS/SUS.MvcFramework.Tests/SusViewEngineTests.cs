namespace SUS.MvcFramework.Tests
{
    using SUS.MvcFramework.ViewEngine;
    using System;
    using System.IO;
    using Xunit;
    public class SusViewEngineTests
    {
        [Theory]
        [InlineData("CleanHtml")]
        [InlineData("Foreach")]
        [InlineData("IfElseFor")]
        [InlineData("ViewModel")]
        public void TestGetHtml(string fileName )
        {

            var viewModel = new TestViewModel
            {
                Name = "Golden Retriever",
                Price = 12345.67M,
                DateOfBirth = new DateTime(2019, 6, 23)


            };

            IViewEngine viewEngine = new SusViewEngine();
            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var actual = viewEngine.GetHtml(view, viewModel);
            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");
            Assert.Equal(expectedResult, actual);
        }

    }
         
}
