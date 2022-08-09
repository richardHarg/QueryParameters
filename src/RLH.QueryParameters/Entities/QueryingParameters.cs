using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters
{
    public class QueryingParameters : QueryingParametersBase
    {
        public string Where
        {
            get { return base.BuildWhereQueryString(); }
            set { base.ParseWhereQueryString(value); }
        }

        public string OrderBy
        {
            get { return base.BuildOrderByQueryString(); }
            set { base.ParseOrderByQueryString(value); }
        }
    }
}
