using RLH.QueryParameters.Core.Options;
using RLH.QueryParameters.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Factories
{
    internal sealed class ParsingOptionsFactory
    {
        public IParsingOptions Create(int version = 1)
        {
            switch (version)
            {
                default: return new ParsingOptions()
                {
                    WherePattern = @"(?<PropertyName>\w+)([ _-])(?<LogicalOperator>\W+)([ _-])(?<PropertyValue>[a-zA-Z0-9/: ._+@-]*$)", // '-' MUST be at the end of propertyValue section
                    WherePatternFriendly = "PropertyName LogicalOperator PropertyValue",
                    OrderByPattern = @"(?<PropertyName>\w+)([ _-])(?<SortOrder>\w+)",
                    OrderByPatternSingle = @"(?<PropertyName>\w+)",
                    OrderByPatternFriendly = "PropertyName SortOrder",
                    SpaceChar = '_',
                    SeperationChar = ','
                };
            }
        }
    }
}
