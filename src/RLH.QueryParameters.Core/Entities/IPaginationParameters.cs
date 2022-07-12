using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.QueryParameters.Core.Entities
{
    public interface IPaginationParameters
    {
        public int PageNumber { get; }
        public int PageSize { get; }
    }
}
