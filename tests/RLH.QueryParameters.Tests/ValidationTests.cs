using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLH.QueryParameters.Core;
using RLH.QueryParameters.Factories;
using RLH.QueryParameters.Tests.DTOs;
using RLH.QueryParameters.Tests.Mocks;

namespace RLH.QueryParameters.Tests
{
    public class ValidationTests
    {
        private readonly IQueryParametersValidator _validator;
        private readonly ISupportedTypeService _supportedTypeService;
        public ValidationTests()
        {
            _validator = new QueryParametersValidatorFactory().GetService();
            _supportedTypeService = new SupportedTypeServiceFactory().GetService();
        }
        [Theory]
        [InlineData(typeof(TestClassA), "TypeString",typeof(string))]
        [InlineData(typeof(TestClassA), "TypeDateTime.Year", typeof(int))]
        [InlineData(typeof(TestClassA), "TestClassB.TypeDateTime.Year", typeof(int))]
        public void FindSupportedTypeForProperty_PropertyName_Valid(Type type,string propertyName,Type expectedType)
        {
            var supportedType = _supportedTypeService.FindSupportedTypeForProperty(type, propertyName);
            Assert.NotNull(supportedType);
            Assert.Equal(expectedType, supportedType.Type);
        }

    }
}
