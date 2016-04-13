using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities
{
    public sealed class PlanetaryPin
    {
        public long PinID { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int SchematicID { get; set; }
        public DateTimeOffset LastLaunchTime { get; set; }
        public int CycleTime { get; set; }
        public int QuantityPerCycle { get; set; }
        public DateTimeOffset InstallTime { get; set; }
        public DateTimeOffset ExpiryTime { get; set; }
        public int ContentTypeID { get; set; }
        public string ContentTypeName { get; set; }
        public int ContentQuantity { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}
