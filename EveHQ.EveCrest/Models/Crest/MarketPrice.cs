using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveHQ.EveCrest.Models.Crest
{
    public class MarketPrice
    {
        public double adjustedPrice { get; set; }
        public double averagePrice { get; set; }
        public ItemType type { get; set; }
    }
}
