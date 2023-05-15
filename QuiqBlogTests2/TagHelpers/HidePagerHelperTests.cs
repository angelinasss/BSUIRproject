using Microsoft.AspNetCore.Razor.TagHelpers;
using NUnit.Framework;
using QuiqBlog.TagHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuiqBlog.Tests.TagHelpers
{
    [TestFixture]
    public class HidePagerHelperTests
    {
        private HidePagerHelper hidePagerHelper;
        private TagHelperContext tagHelperContext;
        private TagHelperOutput tagHelperOutput;

        [SetUp]
        public void Setup()
        {
            hidePagerHelper = new HidePagerHelper();
            tagHelperContext = new TagHelperContext(
                new TagHelperAttributeList(),
                new Dictionary<object, object>(),
                "test");
            tagHelperOutput = new TagHelperOutput(
                "test",
                new TagHelperAttributeList(),
                (useCachedResult, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                });
        }

        [Test]
        public void Process_SuppressesOutput_WhenListCountIsLessThanOrEqualToCount()
        {
            // Arrange
            hidePagerHelper.List = new List<object> { 1, 2, 3 };
            hidePagerHelper.Count = 3;

            // Act
            hidePagerHelper.Process(tagHelperContext, tagHelperOutput);

            // Assert
            Assert.IsTrue(tagHelperOutput.IsContentModified);
            Assert.AreEqual(string.Empty, tagHelperOutput.Content.GetContent());
        }

        [Test]
        public void Process_DoesNotSuppressOutput_WhenListCountIsGreaterThanCount()
        {
            // Arrange
            hidePagerHelper.List = new List<object> { 1, 2, 3 };
            hidePagerHelper.Count = 2;

            // Act
            hidePagerHelper.Process(tagHelperContext, tagHelperOutput);

            // Assert
            Assert.IsFalse(tagHelperOutput.IsContentModified);
            Assert.AreEqual("test", tagHelperOutput.TagName);
        }
    }
}