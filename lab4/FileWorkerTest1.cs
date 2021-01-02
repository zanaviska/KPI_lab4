using NUnit.Framework;
using IIG.FileWorker;
using IIG.BinaryFlag;
using IIG.CoSFE.DatabaseUtils;
using System.Text;

namespace lab4
{
    public class TestFileWorker
    {
        private const string Server = @"DESKTOP-O7BR1H7\TRY4";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool isTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"1111";
        private const int ConnectionTimeout = 75;
        StorageDatabaseUtils storageDatabaseUtils = new StorageDatabaseUtils(Server, Database, isTrusted, Login, Password, ConnectionTimeout);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddFileTest()
        {
            //test regular file
            Assert.IsTrue(storageDatabaseUtils.AddFile("file.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll("..\\..\\..\\file.txt"))));

            //test unicode symbols
            Assert.IsTrue(storageDatabaseUtils.AddFile("妈妈 👌👌ї.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll("..\\..\\..\\妈妈 👌👌ї.txt"))));

            //test fails
            Assert.IsFalse(storageDatabaseUtils.AddFile("妈妈 👌👌ї.txt", null));
        }

        [Test]
        public void GetFile()
        {
            string name;
            byte[] byteText;
            string text;

            //get info with non-existing file
            Assert.IsFalse(storageDatabaseUtils.GetFile(0, out name, out byteText));
            Assert.IsNull(name);
            Assert.IsNull(byteText);

            //get existing info
            Assert.IsTrue(storageDatabaseUtils.GetFile(22, out name, out byteText));
            text = Encoding.Default.GetString(byteText);
            Assert.AreEqual(name, "file.txt");
            Assert.AreEqual(Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll("..\\..\\..\\file.txt")), byteText);

            //get existing infowith non-ASCII
            Assert.IsTrue(storageDatabaseUtils.GetFile(23, out name, out byteText));
            Assert.AreEqual(name, "妈妈 👌👌ї.txt");
            Assert.AreEqual(Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll("..\\..\\..\\妈妈 👌👌ї.txt")), byteText);

            //test receiwing files
            Assert.IsNotNull(storageDatabaseUtils.GetFiles());
        }

        [Test]
        public void TestDelete()
        {
            //test removing non-existing
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(1234567));
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(0));
            Assert.IsTrue(storageDatabaseUtils.DeleteFile(-1234567));

            //test removing existing and non-existing
            Assert.IsTrue(storageDatabaseUtils.AddFile("file.txt", Encoding.Unicode.GetBytes(BaseFileWorker.ReadAll("..\\..\\..\\file.txt"))));
            for (int i = 24; i < 1000; i++)
                Assert.IsTrue(storageDatabaseUtils.DeleteFile(i));
        }
    }
}