using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.PDO.Application.Services.PDO
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; init; } = Enumerable.Empty<T>();
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public bool HasNext => Page * PageSize < Total;
        public bool HasPrev => Page > 1;
    }

}
