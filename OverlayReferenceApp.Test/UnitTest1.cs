using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPass()
        {
            Assert.Pass();
        }

        [Test]
        public void TestFail()
        {
            Assert.Fail();
        }
    }
}