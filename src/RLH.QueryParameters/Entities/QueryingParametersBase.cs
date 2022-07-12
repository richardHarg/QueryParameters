using RLH.QueryParameters.Core.Entities;
using RLH.QueryParameters.Core.Options;
using RLH.QueryParameters.Factories;
using RLH.QueryParameters.Options;
using System.Text.RegularExpressions;


namespace RLH.QueryParameters.Entities
{
    public abstract class QueryingParametersBase : IQueryingParameters
    {
        /// <summary>
        /// Internal Dictionaries holding where and orderby operations
        /// </summary>
        private Dictionary<string, Where> WhereConditionsDictionary  = new Dictionary<string, Where>();
        private Dictionary<string, OrderBy> OrderByConditionsDictionary = new Dictionary<string, OrderBy>();
        private Dictionary<string, string> ValidationErrorsCollection = new Dictionary<string, string>();

        protected IParsingOptions ParsingOptions { get; private set; }

        public QueryingParametersBase()
        {
            ParsingOptions = new ParsingOptionsFactory().Create();
        }

        /// <summary>
        /// Returns a collection of the current Where Conditions
        /// </summary>
        public IEnumerable<IWhere> WhereConditions
        {
            get { return WhereConditionsDictionary.Values; }
        }
        /// <summary>
        /// Returns a collection of the current OrderBy Conditions
        /// </summary>
        public IEnumerable<IOrderBy> OrderByConditions
        {
            get { return OrderByConditionsDictionary.Values; }
        }
        public Dictionary<string,string> ValidationErrors
        {
            get {
                return ValidationErrorsCollection;
                }
        }

        public void AddValidationError(string id, string message)
        {
            ValidationErrorsCollection.Add(id, message);
        }

        /// <summary>
        /// Builds a DynamicLINQ formatted string from the current Where conditions
        /// </summary>
        /// <returns>string</returns>
        public virtual string BuildDynamicWhereString()
        {
            var builtString = "";
            foreach (Where operation in WhereConditionsDictionary.Values)
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
            foreach (Where operation in WhereConditionsDictionary.Values.Where(x => x.External == true))
            {
                if (builtString != "")
                {
                    builtString += ParsingOptions.SeperationChar;
                }

                builtString += $"{operation.PropertyName}{ParsingOptions.SpaceChar}{operation.LogicalOperator}{ParsingOptions.SpaceChar}{operation.PropertyValue.Replace(' ',ParsingOptions.SpaceChar)}";
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
            foreach (string operation in value.Split(ParsingOptions.SeperationChar))
            {
                // Empty or whitespace string as a result of the split operation? Ignore
                if (string.IsNullOrWhiteSpace(operation) == false)
                {
                    // Use Regex and a provided pattern to match the string and attempt to pull out groups with the provided names
                    var values = GetNamedValues(operation, ParsingOptions.WherePattern, new List<string>() { "PropertyName", "LogicalOperator", "PropertyValue" });

                    // FilterBy should be 3 values (as per list above) if this is the case use these to create and add a new FilterBy operation
                    if (values.Count == 3)
                    {
                        // there may be spaces or space chars in the PropertyValue string, for data parsing and comparison these should be replaced
                        // by a space.
                        var formattedValue = values["PropertyValue"].Replace(ParsingOptions.SpaceChar, ' ');

                        AddParsedWhereCondition(values["PropertyName"], values["LogicalOperator"], formattedValue);
                    }
                    // If the number of values is NOT correct then log an error
                    else
                    {
                        ValidationErrorsCollection.Add(operation, $"Unable to parse FilterBy operation as the formatting doesnt match. Values should be formatted as '{ParsingOptions.WherePatternFriendly}' seperated with a '{ParsingOptions.SpaceChar}' character and chained with '{ParsingOptions.SeperationChar}' characters.");
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

            foreach (OrderBy operation in OrderByConditionsDictionary.Values)
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
            foreach (OrderBy operation in OrderByConditionsDictionary.Values.Where(x => x.External == true))
            {
                if (builtString != "")
                {
                    builtString += ParsingOptions.SeperationChar;
                }

                builtString += $"{operation.PropertyName}{ParsingOptions.SpaceChar}{operation.SortOrder}";
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
            foreach (string operation in value.Split(ParsingOptions.SeperationChar))
            {
                // If this split has resulted in an empty string? if so, ignore
                if (string.IsNullOrWhiteSpace(operation) == false)
                {
                    // Use Regex and a provided pattern to match the string and attempt to pull out groups with the provided names
                    var twoPatternValues = GetNamedValues(operation, ParsingOptions.OrderByPattern, new List<string>() { "PropertyName", "SortOrder" });
                    var singlePatternValues = GetNamedValues(operation, ParsingOptions.OrderByPatternSingle, new List<string>() { "PropertyName" });
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
                        ValidationErrorsCollection.Add(operation, $"Unable to parse OrderBy operation as the formatting doesnt match. Values should be formatted as '{ParsingOptions.OrderByPatternFriendly}' seperated with a '{ParsingOptions.SpaceChar}' character and chained with '{ParsingOptions.SeperationChar}' characters.");

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
            AddToConditionDictionary(WhereConditionsDictionary, new Where(propertyName, logicalOperator, propertyValue, true));
        }
        /// <summary>
        /// Adds a new OrderBy condition, call this method from inheriting classes when you need
        /// to add a value parsed from a query string e.g. Externally created.
        /// For adding defaults/manual conditions call the public 'AddOrderBy' method instead
        /// </summary>
        /// <param name="propertyName">Name of the class property to query</param>
        /// <param name="sortOrder">Sort order to apply</param>
        protected void AddParsedOrderByCondition(string propertyName, string sortOrder = "ascending")
        {
            AddToConditionDictionary(OrderByConditionsDictionary, new OrderBy(propertyName, sortOrder, true));
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
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or empty.", nameof(propertyName));
            }

            if (string.IsNullOrEmpty(logicalOperator))
            {
                throw new ArgumentException($"'{nameof(logicalOperator)}' cannot be null or empty.", nameof(logicalOperator));
            }

            if (string.IsNullOrEmpty(propertyValue))
            {
                throw new ArgumentException($"'{nameof(propertyValue)}' cannot be null or empty.", nameof(propertyValue));
            }

            AddToConditionDictionary(WhereConditionsDictionary, new Where(propertyName,logicalOperator,propertyValue,false));
        }
        //// <summary>
        /// Adds a new OrderBy condition, used to ensure default sorting and/or for adding conditions
        /// manually which are not permitted externally. e.g. User cannot sort by X property but a 
        /// condition can be added AFTER validation to sort by that value. These conditions ARE used
        /// when building the DynamicLINQ string but NOT included when building outbound query strings
        /// </summary>
        /// <param name="propertyName">Name of the class property to query</param>
        /// <param name="sortOrder">Sort order to apply</param>
        public void AddManualOrderByCondition(string propertyName,string sortOrder = "ascending")
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException($"'{nameof(propertyName)}' cannot be null or empty.", nameof(propertyName));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                throw new ArgumentException($"'{nameof(sortOrder)}' cannot be null or empty.", nameof(sortOrder));
            }

            AddToConditionDictionary(OrderByConditionsDictionary, new OrderBy(propertyName,sortOrder,false));
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
            if (conditionDictionary is null)
            {
                throw new ArgumentNullException(nameof(conditionDictionary));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

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
