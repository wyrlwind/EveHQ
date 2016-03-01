using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveHQ.EveCrest.Models
{
    class CrestResult<T>
    {
        public int pageCount { get; set; }
        public int totalCount { get; set; }
        public IEnumerable<T> items { get; set; }
    }
}
