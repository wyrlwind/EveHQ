using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities
{
    public sealed class PlanetaryLink
    {
        public long SourcePinID { get; set; }
        public long DestinationPinID { get; set; }
        public int LinkLevel { get; set; }
    }
}
