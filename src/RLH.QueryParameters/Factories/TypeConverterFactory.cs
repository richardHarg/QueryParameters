using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters
{
    internal class TypeConverterFactory
    {
        private bool disposedValue;

        public TypeConverter GetConverterForType(Type type)
        {
            // always check the NON nullable version of a type
            var typeToCheck = IsNullableType(type) ? GetNonNullableType(type) : type;

            if (typeToCheck == typeof(DateTimeOffset))
            {
                return new DateTimeOffsetConverter();
            }
            else if (typeToCheck == typeof(DateTime))
            {
                return new DateTimeConverter();
            }
            else
            {
                return TypeDescriptor.GetConverter(typeToCheck);
            }
        }


        private bool IsNullableType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private Type GetNonNullableType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsNullableType(type) ? type.GetTypeInfo().GetGenericArguments()[0] : type;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TypeConverterFactory()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
