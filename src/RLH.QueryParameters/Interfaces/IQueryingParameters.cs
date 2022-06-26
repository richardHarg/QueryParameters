using RLH.QueryParameters.Entities;
using RLH.QueryParameters.Options;
using RLH.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Interfaces
{
    public interface IQueryingParameters
    {
        public IEnumerable<ValidationError> ValidationErrors { get;}
        public IEnumerable<Where> WhereConditions { get; }
        public IEnumerable<OrderBy> OrderByConditions { get; }

        public void AddValidationError(string id, string message);
        public string BuildDynamicWhereString();
        public string BuildWhereQueryString();
        public string BuildDynamicOrderByString();
        public string BuildOrderByQueryString();

        public void AddManualWhereCondition(string propertyName, string logicalOperator, string propertyValue);
        public void AddManualOrderByCondition(string propertyName, string sortOrder = "ascending");


    }
}
