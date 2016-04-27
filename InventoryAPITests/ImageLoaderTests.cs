using ImageLoader;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InventoryAPITests
{
    [TestClass]
    public class ImageLoaderTests
    {
        [TestInitialize]
        public void Init()
        {
        }

        [TestMethod]
        public void ImageLoaderTest()
        {
            var img = Loader.LoadImageFromFile("TestFiles\\MyImage.txt");
            Assert.AreEqual(3,img.Rows);
            Assert.AreEqual(5, img.Columns);
        }

        //Invlaid Image file
        //Invalid file name..
        //Invalid Ros / Columns
        //Invlaid Data....
        //NAn and other bad format...
    }
}
