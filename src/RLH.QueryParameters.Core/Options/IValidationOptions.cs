using RLH.QueryParameters.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core.Options
{
    public interface IValidationOptions
    {
        public Dictionary<Type, ISupportedType> SupportedTypes { get; }
    }
}
