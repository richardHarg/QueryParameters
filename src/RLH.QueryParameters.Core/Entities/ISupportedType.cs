using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core.Entities
{
    public interface ISupportedType
    {
        public Type Type { get; }
        public List<string> Operators { get; }
        public TypeConverter TypeConverter { get;}
    }
}
