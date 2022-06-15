using RLH.QueryParameters.Factories;
using RLH.QueryParameters.Options;
using RLH.Results;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace RLH.QueryParameters.Entities
{
    public abstract class QueryParametersBase
    {
        private Dictionary<string, Where> _whereConditions  = new Dictionary<string, Where>();
        private Dictionary<string, OrderBy> _orderByConditions = new Dictionary<string, OrderBy>();
        public List<ValidationError> ValidationErrors { get; private set; } = new List<ValidationError>();

        protected ParsingOptions _parsingOptions;

        public QueryParametersBase()
        {
            _parsingOptions = new ParsingOptionsFactory().Create();
        }

        /// <summary>
        /// Returns a collection of the current Where Conditions
        /// </summary>
        public IEnumerable<Where> WhereConditions
        {
            get { return _whereConditions.Values; }
        }
        /// <summary>
        /// Returns a collection of the current OrderBy Conditions
        /// </summary>
        public IEnumerable<OrderBy> OrderByConditions
        {
            get { return _orderByConditions.Values; }
        }
      


        /// <summary>
        /// Builds a DynamicLINQ formatted string from the current Where conditions
        /// </summary>
        /// <returns>string</returns>
        public virtual string BuildDynamicWhereString()
        {
            var builtString = "";
            foreach (Where operation in _whereConditions.Values)
            {
                if (builtString != "")
                {
                    builtString += " && ";
                }

                switch (operation.LogicalOperator)
                {
                    case "*=":
                        builtString += $"{operation.PropertyName}.Contains(\"{operation.PropertyValue}\")";
                        break;
                    default:
                        builtString += $"{operation.PropertyName} {operation.LogicalOperator} \"{operation.PropertyValue}\"";
                        break;
                }
            }
            return builtString;
        }

        /// <summary>
        /// Builds a query formatted string from the current Where conditions
        /// </summary>
        /// <returns>string</returns>
        public virtual string BuildWhereQueryString()
        {
            var builtString = "";
            foreach (Where operation in _whereConditions.Values.Where(x => x.External == true))
            {
                if (builtString != "")
                {
                    builtString += _parsingOptions.SeperationChar;
                }

                builtString += $"{operation.PropertyName}{_parsingOptions.SpaceChar}{operation.LogicalOperator}{_parsingOptions.SpaceChar}{operation.PropertyValue.Replace(' ',_parsingOptions.SpaceChar)}";
            }
            return builtString;
        }
        /// <summary>
        /// Parses an inbound query string containing externally created Where conditions
        /// </summary>
        /// <param name="value">Where query string value</param>
        protected virtual void ParseWhereQueryString(string value)
        {
            // Check if the inbound string is null or empty, if so no processing required
            // and the empty collection/results are returned
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            // Split the values using the designated seperation char
            foreach (string operation in value.Split(_parsingOptions.SeperationChar))
            {
                // Empty or whitespace string as a result of the split operation? Ignore
                if (string.IsNullOrWhiteSpace(operation) == false)
                {
                    // Use Regex and a provided pattern to match the string and attempt to pull out groups with the provided names
                    var values = GetNamedValues(operation, _parsingOptions.WherePattern, new List<string>() { "PropertyName", "LogicalOperator", "PropertyValue" });

                    // FilterBy should be 3 values (as per list above) if this is the case use these to create and add a new FilterBy operation
                    if (values.Count == 3)
                    {
                        // there may be spaces or space chars in the PropertyValue string, for data parsing and comparison these should be replaced
                        // by a space.
                        var formattedValue = values["PropertyValue"].Replace(_parsingOptions.SpaceChar, ' ');

                        AddParsedWhereCondition(values["PropertyName"], values["LogicalOperator"], formattedValue);
                    }
                    // If the number of values is NOT correct then log an error
                    else
                    {
                        ValidationErrors.Add(new ValidationError(operation, $"Unable to parse FilterBy operation as the formatting doesnt match. Values should be formatted as '{_parsingOptions.WherePatternFriendly}' seperated with a '{_parsingOptions.SpaceChar}' character and chained with '{_parsingOptions.SeperationChar}' characters."));
                    }
                }
            }
        }

        /// <summary>
        /// Builds a DynamicLINQ formatted string from the current OrderBy conditions
        /// </summary>
        /// <returns>string</returns>
        public virtual string BuildDynamicOrderByString()
        {
            var builtString = "";

            foreach (OrderBy operation in _orderByConditions.Values)
            {
                if (builtString != "")
                {
                    builtString += ", ";
                }

                builtString += $"{operation.PropertyName} {operation.SortOrder}";
            }

            return builtString;
        }

        /// <summary>
        /// Builds a query formatted string from the current externally created OrderBy conditions
        /// </summary>
        /// <returns>string</returns>
        public virtual string BuildOrderByQueryString()
        {
            var builtString = "";
            foreach (OrderBy operation in _orderByConditions.Values.Where(x => x.External == true))
            {
                if (builtString != "")
                {
                    builtString += _parsingOptions.SeperationChar;
                }

                builtString += $"{operation.PropertyName}{_parsingOptions.SpaceChar}{operation.SortOrder}";
            }
            return builtString;
        }

        /// <summary>
        /// Parses an inbound query string containing OrderBy conditions
        /// </summary>
        /// <param name="value">OrderBy query string value</param>
        protected virtual void ParseOrderByQueryString(string value)
        {

            // Check if the inbound string is null or empty, if so no processing required
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            // Split the values using the designated seperation char
            foreach (string operation in value.Split(_parsingOptions.SeperationChar))
            {
                // If this split has resulted in an empty string? if no ignore
                if (string.IsNullOrWhiteSpace(operation) == false)
                {
                    // Use Regex and a provided pattern to match the string and attempt to pull out groups with the provided names
                    var twoPatternValues = GetNamedValues(operation, _parsingOptions.OrderByPattern, new List<string>() { "PropertyName", "SortOrder" });
                    var singlePatternValues = GetNamedValues(operation, _parsingOptions.OrderByPatternSingle, new List<string>() { "PropertyName" });
                    // OR they can be two values with PropertyName and SortOrder
                    if (twoPatternValues.Count() == 2)
                    {
                        AddParsedOrderByCondition(twoPatternValues["PropertyName"], twoPatternValues["SortOrder"]);
                    }
                    else if (singlePatternValues.Count() == 1)
                    {
                        AddParsedOrderByCondition(singlePatternValues["PropertyName"]);
                    }
                    else
                    {
                        ValidationErrors.Add(new ValidationError(operation, $"Unable to parse OrderBy operation as the formatting doesnt match. Values should be formatted as '{_parsingOptions.OrderByPatternFriendly}' seperated with a '{_parsingOptions.SpaceChar}' character and chained with ',' characters."));

                    }
                }
            }
        }


        /// <summary>
        /// Adds a new Where condition, call this method from inheriting classes when you need
        /// to add a value parsed from a query string e.g. Externally created.
        /// For adding defaults/manual conditions call the public 'AddWhere' method instead
        /// </summary>
        /// <param name="propertyName">Name of the class property to query</param>
        /// <param name="logicalOperator">Logical operator to apply</param>
        /// <param name="propertyValue">Value to check for</param>
        protected void AddParsedWhereCondition(string propertyName, string logicalOperator, string propertyValue)
        {
            AddToConditionDictionary(_whereConditions, new Where(propertyName, logicalOperator, propertyValue, true));
        }
        /// <summary>
        /// Adds a new OrderBy condition, call this method from inheriting classes when you need
        /// to add a value parsed from a query string e.g. Externally created.
        /// For adding defaults/manual conditions call the public 'AddOrderBy' method instead
        /// </summary>
        /// <param name="propertyName">Name of the class property to query</param>
        /// <param name="sortOrder">Sort order to apply</param>
        protected void AddParsedOrderByCondition(string propertyName, string sortOrder)
        {
            AddToConditionDictionary(_orderByConditions, new OrderBy(propertyName, sortOrder, true));
        }
        /// <summary>
        /// Adds a new OrderBy condition, call this method from inheriting classes when you need
        /// to add a value parsed from a query string e.g. Externally created.
        /// For adding defaults/manual conditions call the public 'AddOrderBy' method instead
        /// </summary>
        /// <param name="propertyName">Name of the class property to query</param>
        protected void AddParsedOrderByCondition(string propertyName)
        {
            AddToConditionDictionary(_orderByConditions, new OrderBy(propertyName, "ascending", true));
        }

        //// <summary>
        /// Adds a new Where condition, used to ensure default sorting and/or for adding conditions
        /// manually which are not permitted externally. e.g. User cannot filter by X property but a 
        /// condition can be added AFTER validation to sort by that value. These conditions ARE used
        /// when building the DynamicLINQ string but NOT included when building outbound query strings
        /// </summary>
        /// <param name="propertyName">Name of the class property to query</param>
        /// <param name="logicalOperator">Logical operator to apply</param>
        /// <param name="propertyValue">Value to check for</param>
        public void AddManualWhereCondition(string propertyName,string logicalOperator,string propertyValue)
        {
            AddToConditionDictionary(_whereConditions, new Where(propertyName,logicalOperator,propertyValue,false));
        }
        //// <summary>
        /// Adds a new OrderBy condition, used to ensure default sorting and/or for adding conditions
        /// manually which are not permitted externally. e.g. User cannot sort by X property but a 
        /// condition can be added AFTER validation to sort by that value. These conditions ARE used
        /// when building the DynamicLINQ string but NOT included when building outbound query strings
        /// </summary>
        /// <param name="propertyName">Name of the class property to query</param>
        /// <param name="sortOrder">Sort order to apply</param>
        public void AddManualOrderByCondition(string propertyName,string sortOrder)
        {
            AddToConditionDictionary(_orderByConditions, new OrderBy(propertyName,sortOrder,false));
        }
        //// <summary>
        /// Adds a new Where condition, used to ensure default sorting and/or for adding conditions
        /// manually which are not permitted externally. e.g. User cannot sort by X property but a 
        /// condition can be added AFTER validation to sort by that value. These conditions ARE used
        /// when building the DynamicLINQ string but NOT included when building outbound query strings
        /// </summary>
        /// <param name="propertyName">Name of the class property to query</param>
        public void AddManualOrderByCondition(string propertyName)
        {
            AddToConditionDictionary(_orderByConditions, new OrderBy(propertyName, "ascending", false));
        }

      /*
        public void RemoveWhere(string key)
        {
            RemoveFromConditionDictionary(_whereConditions, key);
        }
        public void RemoveOrderBy(string key)
        {
            RemoveFromConditionDictionary(_orderByConditions, key);
        }
      */



        /// <summary>
        /// Attempts to remove a given entry (type T) from the passed dictionary using the provided
        /// key value
        /// </summary>
        /// <typeparam name="T">Type e.g. Where or OrderBy</typeparam>
        /// <param name="conditionDictionary">Dictionary to remove the value from</param>
        /// <param name="key">Key value used for dictionary</param>
        private void RemoveFromConditionDictionary<T>(Dictionary<string,T> conditionDictionary,string key)
        {
            if (conditionDictionary.ContainsKey(key) == true)
            {
                conditionDictionary.Remove(key);
            }
        }

        /// <summary>
        /// Attempts to add a given value (of T) to the passed dictionary. If the entry already exists
        /// AND overwrite == true then this is removed before adding the new entry
        /// </summary>
        /// <typeparam name="T">Type e.g. Where or OrderBy</typeparam>
        /// <param name="conditionDictionary">Dictionary to add the value to</param>
        /// <param name="value">Instance to T to add</param>
        /// <param name="key">Key to use</param>
        /// <param name="overwrite">If the existing entry (if any) is removed and replace by the 'value' parameter</param>
        private void AddToConditionDictionary<T>(Dictionary<string,T> conditionDictionary,T value) where T : Select
        {
            if (conditionDictionary.ContainsKey(value.PropertyName) == false)
            {
                conditionDictionary.Add(value.PropertyName, value);
            }
        }

        /// <summary>
        /// Performs a REGEX operation with the provided pattern. If this completes looks for groups
        /// with names from the provided and returns a dictionary of these group names and their
        /// values
        /// </summary>
        /// <param name="input">raw string with request operation</param>
        /// <param name="pattern">From QueryOptions - Regex pattern to use</param>
        /// <param name="names">List of group names to look for</param>
        /// <returns>Dictionary with Key of group name and value from regex match group</returns>
        private Dictionary<string, string> GetNamedValues(string input, string pattern, List<string> names)
        {
            var values = new Dictionary<string, string>();
            var match = Regex.Matches(input, pattern).FirstOrDefault();

            // if matching fails return now
            if (match == null)
            {
                return values;
            }
            // Check the match groups for each of the names in the list, if found add to the dictionary
            foreach (string name in names)
            {
                var group = match.Groups[name];
                if (group.Success == true)
                {
                    values.Add(name, group.Value);
                }
            }
            return values;
        }

    }
}
