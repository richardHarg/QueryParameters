using RLH.QueryParameters.Entities;

namespace RLH.QueryParameters.Tests
{
    public class ParsingTests
    {
        [Theory]
        [InlineData("name == paul", "name_==_paul", "name == \"paul\"")]
        [InlineData("name *= john","name_*=_john","name.Contains(\"john\")")]
        [InlineData("age > 25","age_>_25", "age > \"25\"")]
        [InlineData("age >= 100","age_>=_100", "age >= \"100\"")]
        [InlineData("active == true","active_==_true","active == \"true\"")]
        public void Where_Single(string inbound,string outboundQuery,string outboundDynamic)
        {
            QueryingParameters parameters = new QueryingParameters();

            parameters.Where = inbound;

            Assert.Equal(outboundQuery, parameters.BuildWhereQueryString());
            Assert.Equal(outboundDynamic, parameters.BuildDynamicWhereString());
        }
    }
}