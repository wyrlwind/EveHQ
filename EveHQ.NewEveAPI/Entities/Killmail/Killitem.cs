using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities.Killmail
{
    public sealed class KillItem
    {
        public uint TypeID { get; set; }
        public uint Flag { get; set; }
        public uint QtyDropped { get; set; }
        public uint QtyDestroyed { get; set; }
        public int Singleton { get; set; }
    }
}