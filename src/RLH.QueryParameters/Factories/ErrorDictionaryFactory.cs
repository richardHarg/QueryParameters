using RLH.QueryParameters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Factories
{
    internal sealed class ErrorDictionaryFactory
    {
        public ErrorDictionary Create(int version = 1)
        {
            switch (version)
            {
                default:
                    return new ErrorDictionary();
            }
        }
    }
}
