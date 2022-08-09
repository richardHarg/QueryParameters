using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core
{
    public interface IParsingOptions
    {
        /// <summary>
        /// Regex pattern used to parse inbound 'Where' query string
        /// </summary>
        public string WherePattern { get; }
        /// <summary>
        /// Friendly representation of the Where Regex pattern
        /// </summary>
        public string WherePatternFriendly { get; }
        /// <summary>
        /// Regex pattern used to parse inbound 'OrderBy' query string
        /// </summary>
        public string OrderByPattern { get;}
        /// <summary>
        /// Regex pattern used to parse inbound 'OrderBy' query string
        /// </summary>
        public string OrderByPatternSingle { get;}
        /// <summary>
        /// Friendly representation of the OrderBy Regex pattern
        /// </summary>
        public string OrderByPatternFriendly { get; }
        /// <summary>
        /// Character used to divide values within the inbound where/orderby strings e.g. ','
        /// </summary>
        public char SpaceChar { get;}
        /// <summary>
        /// Character used to replace spaces when formatting outbound where/orderby query strings .e.g. '_'
        /// </summary>
        public char SeperationChar { get; }
    }
}
