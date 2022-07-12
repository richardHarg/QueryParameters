using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core.Entities
{
    public interface IWhere : ISelect
    {
        public string LogicalOperator { get; }
        public string PropertyValue { get;}
    }
}
