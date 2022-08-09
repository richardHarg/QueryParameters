using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core
{
    public interface IWhere : ISelect
    {
        public string LogicalOperator { get; }
        public object PropertyValue { get; }
    }
}
