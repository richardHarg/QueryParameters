using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Tests.Mocks
{
    internal class MockQueryingParameters : IQueryingParameters
    {
        public string MockWhereQueryString { get; set; }
        public string MockOrderByQueryString { get; set; }
        public string MockWhereDynamicString { get; set; }
        public string MockOrderByDynamicString { get; set; }

        public Dictionary<string,IWhere> WhereConditionsDictionary = new Dictionary<string, IWhere>();
        public Dictionary<string, IOrderBy> OrderByConditionsDictionary = new Dictionary<string, IOrderBy>();

        private Dictionary<string, string> ValidationErrorsCollection = new Dictionary<string, string>();


        public Dictionary<string, string> ValidationErrors => ValidationErrorsCollection;

        public IEnumerable<IWhere> WhereConditions => WhereConditionsDictionary.Values;

        public IEnumerable<IOrderBy> OrderByConditions => OrderByConditionsDictionary.Values;

        public void AddManualOrderByCondition(string propertyName, string sortOrder = "ascending")
        {
            throw new NotImplementedException();
        }

        public void AddManualWhereCondition(string propertyName, string logicalOperator, string propertyValue)
        {
            throw new NotImplementedException();
        }

        public void AddValidationError(string id, string message)
        {
            ValidationErrorsCollection.Add(id, message);
        }

        public string BuildDynamicOrderByString()
        {
            return MockOrderByDynamicString;
        }

        public string BuildDynamicWhereString()
        {
            return MockWhereDynamicString;
        }

        public string BuildOrderByQueryString()
        {
            return MockOrderByQueryString;
        }

        public string BuildWhereQueryString()
        {
            return MockWhereQueryString;
        }
    }
}
