using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLH.QueryParameters.Entities;

namespace RLH.QueryParameters.Tests
{
    public class TestParameters : QueryingParameters
    {
        public TestParameters()
        {
            ParsingOptions.OrderByPatternFriendly = "THIS IS A TEST";
        }

        public string TestValue
        {
            get { return ParsingOptions.OrderByPatternFriendly; }
        }
    }
}
