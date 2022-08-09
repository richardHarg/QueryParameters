
using RLH.QueryParameters.Core;

namespace RLH.QueryParameters
{
    /// <summary>
    /// Factory class to create new QueryOptions instances
    /// </summary>
    public class SupportedTypeOptionsFactory : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Creates a new QueryOptions instance, default to v1
        /// </summary>
        /// <param name="version">Version to create</param>
        /// <returns>QueryOptions</returns>
        public ISupportedTypeOptions Create(int version = 1)
        {
            switch (version)
            {
                default:
                    return new SupportedTypeOptions()
                    {
                        SupportedTypes = GetSupportedTypes()
                    };
            }
        }

        public Dictionary<Type,ISupportedType> GetSupportedTypes()
        {
            var converterFactory = new TypeConverterFactory();
            
                return new Dictionary<Type, ISupportedType>()
            {
                {typeof(string),new SupportedType(typeof(string), new List<string>() { "==", "*=" },converterFactory.GetConverterForType(typeof(string)))},
                {typeof(int),new SupportedType(typeof(int), new List<string>() { "<", ">", "==", ">=", "<="  },converterFactory.GetConverterForType(typeof(int))) },
                {typeof(double),new SupportedType(typeof(double), new List<string>() { "<", ">", "==", ">=", "<="  },converterFactory.GetConverterForType(typeof(double))) },
                {typeof(float),new SupportedType(typeof(float), new List<string>() { "<", ">", "==", ">=", "<="  },converterFactory.GetConverterForType(typeof(float))) },
                {typeof(decimal),new SupportedType(typeof(decimal), new List<string>() { "<", ">", "==", ">=", "<="  },converterFactory.GetConverterForType(typeof(decimal))) },
                {typeof(DateTime),new SupportedType(typeof(DateTime), new List<string>() { "<", ">", "==", ">=", "<="  },converterFactory.GetConverterForType(typeof(DateTime))) },
                {typeof(DateTimeOffset),new SupportedType(typeof(DateTimeOffset), new List<string>() { "<", ">", "==", ">=", "<="  },converterFactory.GetConverterForType(typeof(DateTimeOffset))) },
                {typeof(bool),new SupportedType(typeof(bool), new List<string>() { "==" },converterFactory.GetConverterForType(typeof(bool))) }
            };
            
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
        // ~QueryOptionsFactory()
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
