using RLH.QueryParameters.Core.Entities;
using RLH.QueryParameters.Core.Options;
using RLH.QueryParameters.Entities;
using RLH.QueryParameters.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Factories
{
    /// <summary>
    /// Factory class to create new QueryOptions instances
    /// </summary>
    public class ValidationOptionsFactory : IDisposable
    {
        private bool disposedValue;

        /// <summary>
        /// Creates a new QueryOptions instance, default to v1
        /// </summary>
        /// <param name="version">Version to create</param>
        /// <returns>QueryOptions</returns>
        public IValidationOptions Create(int version = 1)
        {
            switch (version)
            {
                default:
                    return new ValidationOptions()
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

     

        private Predicate<string> StringPredicate
        {
            get { return (string value) => { return true; }; }
        }
        private Predicate<string> IntPredicate
        {
            get { return (string value) => { return int.TryParse(value, out int result); }; }
        }
        private Predicate<string> DoublePredicate
        {
            get { return (string value) => { return double.TryParse(value, out Double result); }; }
        }
        private Predicate<string> FloatPredicate
        {
            get { return (string value) => { return float.TryParse(value, out float result); }; }
        }
        private Predicate<string> DecimalPredicate
        {
            get { return (string value) => { return decimal.TryParse(value, out decimal result); }; }
        }
        private  Predicate<string> BoolPredicate
        {
            get { return (string value) => { return bool.TryParse(value, out bool result); }; }
        }
        private  Predicate<string> DateTimePredicate
        {
            get { return (string value) => {
                var formats = new string[]
        {
                    "yyyy/MM/dd",
                    "yyyy/M/dd",
                    "yyyy/M/d",

                    "yyyy/MM/dd-HH:mm:ss",
                    "yyyy/M/dd-HH:mm:ss",
                    "yyyy/M/d-HH:mm:ss",

                    "yyyy/MM/dd-H:mm:ss",
                    "yyyy/M/dd-H:mm:ss",
                    "yyyy/M/d-H:mm:ss",

                    "yyyy/MM/dd-HH:m:ss",
                    "yyyy/M/dd-HH:m:ss",
                    "yyyy/M/d-HH:m:ss",

                    "yyyy/MM/dd-H:m:ss",
                    "yyyy/M/dd-H:m:ss",
                    "yyyy/M/d-H:m:ss",

                     "yyyy/MM/dd-HH:mm:s",
                    "yyyy/M/dd-HH:mm:s",
                    "yyyy/M/d-HH:mm:s",

                    "yyyy/MM/dd-H:mm:s",
                    "yyyy/M/dd-H:mm:s",
                    "yyyy/M/d-H:mm:s",

                    "yyyy/MM/dd-HH:m:s",
                    "yyyy/M/dd-HH:m:s",
                    "yyyy/M/d-HH:m:s",

                    "yyyy/MM/dd-H:m:s",
                    "yyyy/M/dd-H:m:s",
                    "yyyy/M/d-H:m:s",

                    // withoutSeconds

                    "yyyy/MM/dd-HH:mm",
                    "yyyy/M/dd-HH:mm",
                    "yyyy/M/d-HH:mm",

                    "yyyy/MM/dd-H:mm",
                    "yyyy/M/dd-H:mm",
                    "yyyy/M/d-H:mm",

                    "yyyy/MM/dd-HH:m",
                    "yyyy/M/dd-HH:m",
                    "yyyy/M/d-HH:m",

                    "yyyy/MM/dd-H:m",
                    "yyyy/M/dd-H:m",
                    "yyyy/M/d-H:m",

                     "yyyy/MM/dd-HH:mm",
                    "yyyy/M/dd-HH:mm",
                    "yyyy/M/d-HH:mm",

                    "yyyy/MM/dd-H:mm",
                    "yyyy/M/dd-H:mm",
                    "yyyy/M/d-H:mm",

                    "yyyy/MM/dd-HH:m",
                    "yyyy/M/dd-HH:m",
                    "yyyy/M/d-HH:m",

                    "yyyy/MM/dd-H:m",
                    "yyyy/M/dd-H:m",
                    "yyyy/M/d-H:m",
        };
                return DateTime.TryParseExact(value, formats, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out DateTime dateTimeResult);
            }; }
        }
        private  Predicate<string> DateTimeOffsetPredicate
        {
            get
            {
                return (string value) => {
                    var formats = new string[]
                {
                    "yyyy/MM/dd",
                    "yyyy/M/dd",
                    "yyyy/M/d",

                    "yyyy/MM/dd-HH:mm:sszzz",
                    "yyyy/M/dd-HH:mm:sszzz",
                    "yyyy/M/d-HH:mm:sszzz",

                    "yyyy/MM/dd-H:mm:sszzz",
                    "yyyy/M/dd-H:mm:sszzz",
                    "yyyy/M/d-H:mm:sszzz",

                    "yyyy/MM/dd-HH:m:ss zzz",
                    "yyyy/M/dd-HH:m:ss zzz",
                    "yyyy/M/d-HH:m:ss zzz",

                    "yyyy/MM/dd-H:m:sszzz",
                    "yyyy/M/dd-H:m:sszzz",
                    "yyyy/M/d-H:m:sszzz",

                     "yyyy/MM/dd-HH:mm:szzz",
                    "yyyy/M/dd-HH:mm:szzz",
                    "yyyy/M/d-HH:mm:szzz",

                    "yyyy/MM/dd-H:mm:szzz",
                    "yyyy/M/dd-H:mm:szzz",
                    "yyyy/M/d-H:mm:szzz",

                    "yyyy/MM/dd-HH:m:szzz",
                    "yyyy/M/dd-HH:m:szzz",
                    "yyyy/M/d-HH:m:szzz",

                    "yyyy/MM/dd-H:m:szzz",
                    "yyyy/M/dd-H:m:szzz",
                    "yyyy/M/d-H:m:szzz",

                     // withoutSeconds

                    "yyyy/MM/dd-HH:mmzzz",
                    "yyyy/M/dd-HH:mmzzz",
                    "yyyy/M/d-HH:mmzzz",

                    "yyyy/MM/dd-H:mmzzz",
                    "yyyy/M/dd-H:mmzzz",
                    "yyyy/M/d-H:mmzzz",

                    "yyyy/MM/dd-HH:mzzz",
                    "yyyy/M/dd-HH:mzzz",
                    "yyyy/M/d-HH:mzzz",

                    "yyyy/MM/dd-H:mzzz",
                    "yyyy/M/dd-H:mzzz",
                    "yyyy/M/d-H:mzzz",

                     "yyyy/MM/dd-HH:mmzzz",
                    "yyyy/M/dd-HH:mmzzz",
                    "yyyy/M/d-HH:mmzzz",

                    "yyyy/MM/dd-H:mmzzz",
                    "yyyy/M/dd-H:mmzzz",
                    "yyyy/M/d-H:mmzzz",

                    "yyyy/MM/dd-HH:mzzz",
                    "yyyy/M/dd-HH:mzzz",
                    "yyyy/M/d-HH:mzzz",

                    "yyyy/MM/dd-H:mzzz",
                    "yyyy/M/dd-H:mzzz",
                    "yyyy/M/d-H:mzzz",

                    // no offset

                    "yyyy/MM/dd-HH:mm:ss",
                    "yyyy/M/dd-HH:mm:ss",
                    "yyyy/M/d-HH:mm:ss",

                    "yyyy/MM/dd-H:mm:ss",
                    "yyyy/M/dd-H:mm:ss",
                    "yyyy/M/d-H:mm:ss",

                    "yyyy/MM/dd-HH:m:ss",
                    "yyyy/M/dd-HH:m:ss",
                    "yyyy/M/d-HH:m:ss",

                    "yyyy/MM/dd-H:m:ss",
                    "yyyy/M/dd-H:m:ss",
                    "yyyy/M/d-H:m:ss",

                     "yyyy/MM/dd-HH:mm:s",
                    "yyyy/M/dd-HH:mm:s",
                    "yyyy/M/d-HH:mm:s",

                    "yyyy/MM/dd-H:mm:s",
                    "yyyy/M/dd-H:mm:s",
                    "yyyy/M/d-H:mm:s",

                    "yyyy/MM/dd-HH:m:s",
                    "yyyy/M/dd-HH:m:s",
                    "yyyy/M/d-HH:m:s",

                    "yyyy/MM/dd-H:m:s",
                    "yyyy/M/dd-H:m:s",
                    "yyyy/M/d-H:m:s",

                     // withoutSeconds

                    "yyyy/MM/dd-HH:mm",
                    "yyyy/M/dd-HH:mm",
                    "yyyy/M/d-HH:mm",

                    "yyyy/MM/dd-H:mm",
                    "yyyy/M/dd-H:mm",
                    "yyyy/M/d-H:mm",

                    "yyyy/MM/dd-HH:m",
                    "yyyy/M/dd-HH:m",
                    "yyyy/M/d-HH:m",

                    "yyyy/MM/dd-H:m",
                    "yyyy/M/dd-H:m",
                    "yyyy/M/d-H:m",

                     "yyyy/MM/dd-HH:mm",
                    "yyyy/M/dd-HH:mm",
                    "yyyy/M/d-HH:mm",

                    "yyyy/MM/dd-H:mm",
                    "yyyy/M/dd-H:mm",
                    "yyyy/M/d-H:mm",

                    "yyyy/MM/dd-HH:m",
                    "yyyy/M/dd-HH:m",
                    "yyyy/M/d-HH:m",

                    "yyyy/MM/dd-H:m",
                    "yyyy/M/dd-H:m",
                    "yyyy/M/d-H:m"
                };
                    return DateTimeOffset.TryParseExact(value, formats, CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.None, out DateTimeOffset dateTimeResult);
                };
            }
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
