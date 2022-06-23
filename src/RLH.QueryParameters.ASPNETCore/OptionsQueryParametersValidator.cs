using Microsoft.Extensions.Options;
using RLH.QueryParameters.Options;
using RLH.QueryParameters.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.ASPNETCore
{
    public class OptionsQueryParametersValidator : QueryParametersValidator
    {
        public OptionsQueryParametersValidator(IOptions<ValidationOptions> options) : base(options.Value)
        {

        }
    }
}
