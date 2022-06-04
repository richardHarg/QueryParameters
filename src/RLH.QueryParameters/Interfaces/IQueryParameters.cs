using RLH.QueryParameters.Entities;
using RLH.QueryParameters.Options;
using RLH.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Interfaces
{
    public interface IQueryParameters
    {
        public List<ValidationError> ValidationErrors { get;}
        public IEnumerable<Where> WhereConditions { get; }
        public IEnumerable<OrderBy> OrderByConditions { get; }

        public string BuildDynamicWhereString();
        public string BuildWhereQueryString();
        public string BuildDynamicOrderByString();
        public string BuildOrderByQueryString();

        public void AddManualWhereCondition(string propertyName, string logicalOperator, string propertyValue);
        public void AddManualOrderByCondition(string propertyName, string sortOrder);
        public void AddManualOrderByCondition(string propertyName);


    }
}
