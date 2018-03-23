using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment6Database;

namespace DatabaseTester
{
    [TestFixture]
    public class TestClass
    {

        [Test]
        public void TestMethod()
        {
           OleDbConnection conn = Program.CreateConnection();
           int x= Program.FeesCalculation(conn);
            // TODO: Add your test code here
            Assert.Pass("Your first passing test");
        }
    }
}
