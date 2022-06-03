
namespace RLH.QueryParameters.Options
{
    public sealed class ParsingOptions
    {
        /// <summary>
        /// Regex pattern used to parse inbound 'Where' query string
        /// </summary>
        public string WherePattern { get; set; }
        /// <summary>
        /// Friendly representation of the Where Regex pattern
        /// </summary>
        public string WherePatternFriendly { get; set; }
        /// <summary>
        /// Regex pattern used to parse inbound 'OrderBy' query string
        /// </summary>
        public string OrderByPattern { get; set; }
        /// <summary>
        /// Regex pattern used to parse inbound 'OrderBy' query string
        /// </summary>
        public string OrderByPatternSingle { get; set; }
        /// <summary>
        /// Friendly representation of the OrderBy Regex pattern
        /// </summary>
        public string OrderByPatternFriendly { get; set; }
        /// <summary>
        /// Character used to divide values within the inbound where/orderby strings e.g. ','
        /// </summary>
        public char SpaceChar { get; set; }
        /// <summary>
        /// Character used to replace spaces when formatting outbound where/orderby query strings .e.g. '_'
        /// </summary>
        public char SeperationChar { get; set; }
    }
}
