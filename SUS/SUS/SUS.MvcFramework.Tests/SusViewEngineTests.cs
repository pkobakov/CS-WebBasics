namespace SUS.MvcFramework.Tests
{
    using SUS.MvcFramework.ViewEngine;
    using System;
    using System.Collections.Generic;
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

        [Fact]
        public void TestTemplateviewModel() 
        {
            IViewEngine viewEngine = new SusViewEngine();
            var actual = viewEngine
                .GetHtml(@"@foreach(var num in Model){
<span>@num</span>
}", new List<int> { 1,2,3});

            var expectedResult = @"<span>1</span>
<span>2</span>
<span>3</span>
";

            Assert.Equal(expectedResult, actual);
        }

    }
         
}
