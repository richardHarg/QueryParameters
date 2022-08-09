using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core
{
    public interface IQueryingParameters
    {
        public Dictionary<string, string> ValidationErrors { get; }
        public IEnumerable<IWhere> WhereConditions { get; }
        public IEnumerable<IOrderBy> OrderByConditions { get; }

        public void AddValidationError(string id, string message);
        public string BuildDynamicWhereString();
        public string BuildWhereQueryString();
        public string BuildDynamicOrderByString();
        public string BuildOrderByQueryString();

        public void AddManualWhereCondition(string propertyName, string logicalOperator, string propertyValue);
        public void AddManualOrderByCondition(string propertyName, string sortOrder = "ascending");


    }
}
