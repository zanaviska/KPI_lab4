using NUnit.Framework;
using System.Text;
using IIG.BinaryFlag;
using IIG.FileWorker;
using System.IO;

namespace lab4
{
    class TestBinaryFlag
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BinaryFlagTest()
        {
            //test flag storing
            MultipleBinaryFlag mbf0 = new MultipleBinaryFlag(150, false);
            MultipleBinaryFlag mbf1 = new MultipleBinaryFlag(6, true);
            mbf0.SetFlag(5);

            Assert.True(BaseFileWorker.Write(mbf0.GetFlag().ToString(), "testfile.txt"));
            Assert.AreEqual(mbf0.GetFlag().ToString(), BaseFileWorker.ReadAll("testfile.txt"));

            Assert.True(BaseFileWorker.Write(mbf1.GetFlag().ToString(), "testfile.txt"));
            Assert.AreEqual(mbf1.GetFlag().ToString(), BaseFileWorker.ReadAll("testfile.txt"));

            File.Delete("testfile.txt");
        }
    }
}
