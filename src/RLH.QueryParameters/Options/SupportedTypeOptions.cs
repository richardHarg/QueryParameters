
using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;

namespace RLH.QueryParameters
{
    public sealed class SupportedTypeOptions : ISupportedTypeOptions
    {
        /// <summary>
        /// Dictionary of supported inbound Where property values. Contains the type, valid operations (e.g. ==) and
        /// a predicate method used to ensure the string value parses to the correct base type e.g. string > int.
        /// </summary>
        public Dictionary<Type, ISupportedType> SupportedTypes { get; set; } = new Dictionary<Type, ISupportedType>();
    }
}
