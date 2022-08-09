using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RLH.QueryParameters
{
    /// <summary>
    /// Used for DynamicLINQ parsing this class contains details of a specific data type.
    /// These include valid filtering operations e.g. '==' 
    /// as well as a predicate method to test if the inbound string parses to the correct datatype
    /// </summary>
    public sealed class SupportedType : ISupportedType
    {
        /// <summary>
        /// Create a new SupportedType associated with a given data type.
        /// </summary>
        /// <param name="type">Data type e.g. int</param>
        /// <param name="supportedOperators">list of logical operators valid for this type</param>
        /// <param name="tryParse">Predicate method used to parse string > this type</param>
        public SupportedType(Type type, List<string> supportedOperators, TypeConverter typeConverter)
        {
            Type = type;
            Operators = supportedOperators ?? throw new ArgumentNullException(nameof(supportedOperators));
            TypeConverter = typeConverter;
        }

        /// <summary>
        /// Data type e.g. int
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// list of logical operators valid for this type
        /// </summary>
        public List<string> Operators { get; private set; }
        /// <summary>
        /// Predicate method used to parse string > this type
        /// </summary>
        public  TypeConverter TypeConverter { get; private set; }
    }
}
