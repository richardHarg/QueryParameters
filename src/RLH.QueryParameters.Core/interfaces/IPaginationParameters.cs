using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core
{
    public interface IPaginationParameters
    {
        public int PageNumber { get; }
        public int PageSize { get; }
    }
}
