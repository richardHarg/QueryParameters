using RLH.QueryParameters.Entities;
using RLH.QueryParameters.Interfaces;
using RLH.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Services
{
    public interface IQueryParametersValidator : IDisposable
    {
        public List<ValidationError> Validate<T>(IQueryParameters queryParameters);
    }
}
