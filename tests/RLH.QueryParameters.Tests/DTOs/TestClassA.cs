using RLH.QueryParameters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Tests.DTOs
{
    internal class TestClassA
    {
        [Queryable]
        public TestClassB TestClassB { get; set; }
        [Queryable]
        public string TypeString { get; set; }
        [Queryable]
        public int TypeInt { get; set; }
        [Queryable]
        public double TypeDouble { get; set; }
        [Queryable]
        public float TypeFloat { get; set; }
        [Queryable]
        public decimal TypeDecimal { get; set; }
        [Queryable]
        public DateTime TypeDateTime { get; set; }
        [Queryable]
        public DateTimeOffset TypeDateTimeOffset { get; set; }
        [Queryable]
        public bool TypeBoolean { get; set; }

        public string NotQueryable { get; set; }
    }
}
