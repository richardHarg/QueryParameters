using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Tests.Mocks
{
    internal class MockWhere : IWhere
    {
        public string LogicalOperator {get; set;}

        public object PropertyValue { get; set; }

        public bool External { get; set; } = false;

        public string PropertyName { get; set; }
    }
}
