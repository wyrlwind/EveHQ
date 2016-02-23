using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities
{
    public sealed class Blueprint
    {
        public long ItemID { get; set; }
        public long LocationID { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public long FlagID { get; set; }
        public long Quantity { get; set; }
        public long TimeEfficiency { get; set; }
        public long MaterialEfficiency { get; set; }
        public long Runs { get; set; }
    }
}