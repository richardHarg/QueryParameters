using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters
{
    public class SupportedTypeServiceFactory
    {
        public ISupportedTypeService GetService(ISupportedTypeOptions options, int version = 1)
        {
            switch (version)
            {
                default:
                    return new SupportedTypeService(options);
            }
        }
        public ISupportedTypeService GetService(int version = 1)
        {
            return GetService(new SupportedTypeOptionsFactory().Create(), version);

        }
    }
}
