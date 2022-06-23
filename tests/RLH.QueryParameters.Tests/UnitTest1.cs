namespace RLH.QueryParameters.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var testPara = new TestParameters();

            Assert.Equal("THIS IS A TEST", testPara.TestValue);
        }
    }
}