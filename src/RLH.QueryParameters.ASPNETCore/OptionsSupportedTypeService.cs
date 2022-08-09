using Microsoft.Extensions.Options;
using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.ASPNETCore
{
    public class OptionsSupportedTypeService : SupportedTypeService
    {
        public OptionsSupportedTypeService(IOptions<SupportedTypeOptions> options) : base(options.Value)
        {
        }
    }
}
