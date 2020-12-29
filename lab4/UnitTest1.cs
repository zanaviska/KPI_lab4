using NUnit.Framework;
using IIG.FileWorker;
using IIG.BinaryFlag;
using IIG.CoSFE.DatabaseUtils;

namespace lab4
{
    public class Tests
    {
        private const string Server = @"DESKTOP-O7BR1H7\TRY4";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool isTrusted = false;
        private const string Login = @"coswe";
        private const string Password = @"L}EjpfCgru9X@GLj";
        private const int ConnectionTimeout = 75;

        [SetUp]
        public void Setup()
        {
            FlagpoleDatabaseUtils flagpoleDatabaseUtils = new FlagpoleDatabaseUtils(Server, Database, isTrusted, Login, Password, ConnectionTimeout);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}