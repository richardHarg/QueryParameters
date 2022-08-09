using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters
{
    public class SupportedTypeService : ISupportedTypeService
    {
        private ISupportedTypeOptions _options;
        public SupportedTypeService(ISupportedTypeOptions options)
        {
            _options = options;
        }



        /// <summary>
        ///  Checks the provided dictionary of queryable class properties for the given 
        ///  PropertyName key and, if found returns the type of this property.
        ///  
        ///  If above key located checks the Options 'SupportedTypes' dictionary for the property type obtained above
        ///  and, if found returns the 'SupportedType' instance associated with this data type.
        /// </summary>
        /// <param name="propertyName">Name of Property to search for</param>
        /// <param name="queryableProperties">Dictionary with PropertyNames/Types of valid queryable properties</param>
        /// <returns></returns>
        public ISupportedType FindSupportedTypeForProperty(Type initialType, string propertyName)
        {
            // we need to cascade down through propertyNames to reach the correct type to query
            // This includes checking the name/s are valid for the objects they are connected to
            // e.g. datetimeoffset.Date
            // or nested properties 2ndclassname.2ndclasspropertyName 

            // holds the individual values of the propertyName value passed
            string[] split = propertyName.Split('.');
            // To begin with this is set to the type passed into the method
            Type cascadedType = initialType;
            // The final supported type found (if any!) - initially set to null and can be returned as such if
            // no base type is located
            ISupportedType supportedType = null;

            // e.g.
            // User.Created (datetimeoffset).year

            // keep cascading down property until either base type reached OR error causes a break
            for (int i = 0; i < split.Length; i++)
            {
                // This will return a list of property names (lowercase) and their type.
                Dictionary<string, Type> classProperties = GetCurrentClassProperties(cascadedType);

                // IF a type is located from the above dictionary with a name matching the position
                // in the 'split' array being checked then...
                if (classProperties.TryGetValue(split[i].ToLower(), out Type type) == true)
                {
                    // CascadedType set to this type and the loop (may) continue.
                    cascadedType = type;
                }
                // If no type is found the loop is broken and no supported type is returned
                else
                {
                    return null;
                }
            }

            //We should now have the base most type in the 'cascadedType' field. Use this to try and locate
            //A supportedType from the dictionary
            _options.SupportedTypes.TryGetValue(cascadedType, out supportedType);

            return supportedType;
        }

        private Dictionary<string, Type> GetCurrentClassProperties(Type type)
        {
            // The type passed may be a custom class with queryable properties
            // As such both ALL property types AND ONLY queryable types are logged
            // IF ANY property is marked as queryable then only the list of queryable 
            // types is returned, if none then its fair game and all are returned.
            Dictionary<string, Type> types = new Dictionary<string, Type>();
            Dictionary<string, Type> queryableTypes = new Dictionary<string, Type>();

            // loop through all class properties using reflection
            foreach (PropertyInfo property in type.GetProperties())
            {
                // Add the name and type of this property to the dictionary
                types.Add(property.Name.ToLower(), property.PropertyType);

                // If the property is marked as 'queryable' then ALSO add it to the queryable dictionary
                if (property.CustomAttributes.Any(x => x.AttributeType == typeof(QueryableAttribute)))
                {
                    queryableTypes.Add(property.Name.ToLower(), property.PropertyType);
                }
            }

            // If ANY queryable properties were found return ONLY the list of queryable types, if not return all.
            if (queryableTypes.Any())
            {
                return queryableTypes;
            }
            else
            {
                return types;
            }
        }

    }
}
