using NUnit.Framework;
using System.Text;
using IIG.BinaryFlag;
using IIG.CoSFE.DatabaseUtils;
using System.IO;

namespace lab4
{
    class TestBinaryFlag
    {
        private const string Server = @"DESKTOP-O7BR1H7\TRY4";
        private const string Database = @"IIG.CoSWE.FlagpoleDB";
        private const bool isTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"1111";
        private const int ConnectionTimeout = 75;
        //StorageDatabaseUtils storageDatabaseUtils = new StorageDatabaseUtils(Server, Database, isTrusted, Login, Password, ConnectionTimeout);
        FlagpoleDatabaseUtils flagpoleUtils = new FlagpoleDatabaseUtils(Server, Database, isTrusted, Login, Password, ConnectionTimeout);
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BinaryFlagTest()
        {
           
            //test flag storing
            MultipleBinaryFlag mbf0 = new MultipleBinaryFlag(6, true);
            MultipleBinaryFlag mbf1 = new MultipleBinaryFlag(6, false);
            MultipleBinaryFlag mbf2 = new MultipleBinaryFlag(50, true);
            MultipleBinaryFlag mbf3 = new MultipleBinaryFlag(50, false);
            MultipleBinaryFlag mbf4 = new MultipleBinaryFlag(150, true);
            MultipleBinaryFlag mbf5 = new MultipleBinaryFlag(150, false);
            mbf1.SetFlag(5);
            mbf3.SetFlag(5);
            mbf5.SetFlag(5);
            //add from variable
            Assert.IsTrue(flagpoleUtils.AddFlag(mbf0.ToString(), mbf0.GetFlag()));
            Assert.IsTrue(flagpoleUtils.AddFlag(mbf1.ToString(), mbf1.GetFlag()));
            Assert.IsTrue(flagpoleUtils.AddFlag(mbf2.ToString(), mbf2.GetFlag()));
            Assert.IsTrue(flagpoleUtils.AddFlag(mbf3.ToString(), mbf3.GetFlag()));
            Assert.IsTrue(flagpoleUtils.AddFlag(mbf4.ToString(), mbf4.GetFlag()));
            Assert.IsTrue(flagpoleUtils.AddFlag(mbf5.ToString(), mbf5.GetFlag()));

            //add manually
            Assert.IsTrue(flagpoleUtils.AddFlag("FTF", false));
            Assert.IsTrue(flagpoleUtils.AddFlag("TTTTT", true));
            Assert.IsTrue(flagpoleUtils.AddFlag("FFFF", false));

            string view;
            //test variable added
            Assert.IsTrue(flagpoleUtils.GetFlag(3, out view, out bool? value));
            Assert.AreEqual(mbf0.ToString(), view);
            Assert.AreEqual(mbf0.GetFlag(), value);
            Assert.IsTrue(flagpoleUtils.GetFlag(4, out view, out value));
            Assert.AreEqual(mbf1.ToString(), view);
            Assert.AreEqual(mbf1.GetFlag(), value);
            Assert.IsTrue(flagpoleUtils.GetFlag(5, out view, out value));
            Assert.AreEqual(mbf2.ToString(), view);
            Assert.AreEqual(mbf2.GetFlag(), value);
            Assert.IsTrue(flagpoleUtils.GetFlag(6, out view, out value));
            Assert.AreEqual(mbf3.ToString(), view);
            Assert.AreEqual(mbf3.GetFlag(), value);
            Assert.IsTrue(flagpoleUtils.GetFlag(7, out view, out value));
            Assert.AreEqual(mbf4.ToString(), view);
            Assert.AreEqual(mbf4.GetFlag(), value);
            Assert.IsTrue(flagpoleUtils.GetFlag(8, out view, out value));
            Assert.AreEqual(mbf5.ToString(), view);
            Assert.AreEqual(mbf5.GetFlag(), value);
            //test manually added
            Assert.IsTrue(flagpoleUtils.GetFlag(9, out view, out value));
            Assert.AreEqual("FTF", view);
            Assert.AreEqual(false, value);
            Assert.IsTrue(flagpoleUtils.GetFlag(10, out view, out value));
            Assert.AreEqual("TTTTT", view);
            Assert.AreEqual(true, value);
            Assert.IsTrue(flagpoleUtils.GetFlag(11, out view, out value));
            Assert.AreEqual("FFFF", view);
            Assert.AreEqual(false, value);
        }
    }
}
