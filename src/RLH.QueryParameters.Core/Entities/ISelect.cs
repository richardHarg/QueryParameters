using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core.Entities
{
    public interface ISelect
    {
        public bool External { get; }
        public string PropertyName { get;}
    }
}
