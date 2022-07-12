using RLH.QueryParameters.Core.Entities;
using RLH.QueryParameters.Core.Options;
using RLH.QueryParameters.Entities;
using System;
using System.Collections.Generic;

namespace RLH.QueryParameters.Options
{
    public sealed class ValidationOptions : IValidationOptions
    {
        /// <summary>
        /// Dictionary of supported inbound Where property values. Contains the type, valid operations (e.g. ==) and
        /// a predicate method used to ensure the string value parses to the correct base type e.g. string > int.
        /// </summary>
        public Dictionary<Type, ISupportedType> SupportedTypes { get; set; } = new Dictionary<Type, ISupportedType>();
    }
}
