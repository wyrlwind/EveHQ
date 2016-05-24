using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities
{
    public sealed class PlanetaryRoute
    {
        public long RouteID { get; set; }
        public long SourcePinID { get; set; }
        public long DestinationPinID { get; set; }
        public int ContentTypeID { get; set; }
        public string ContentTypeName { get; set; }
        public int Quantity { get; set; }
        public long Waypoint1 { get; set; }
        public long Waypoint2 { get; set; }
        public long Waypoint3 { get; set; }
        public long Waypoint4 { get; set; }
        public long Waypoint5 { get; set; }
    }
}
