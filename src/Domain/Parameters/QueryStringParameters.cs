using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Parameters
{
    public class QueryStringParameters
    {
        const int cMaxPageSize = 50;

        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get
            {
                return cPageSize;
            }
            set
            {
                cPageSize = (value > cMaxPageSize) ? cMaxPageSize : value;
            }
        }
        private int cPageSize = 10;

    }
}
