using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities
{
    public sealed class PlanetaryColony
    {
        public int SolarSystemID { get; set; }
        public string SolarSystemName { get; set; }
        public int PlanetID { get; set; }
        public string PlanetName { get; set; }
        public int PlanetTypeID { get; set; }
        public string PlanetTypeName { get; set; }
        public long OwnerID { get; set; }
        public string OwnerName { get; set; }
        public DateTimeOffset LastUpdate { get; set; }
        public int UpgradeLevel { get; set; }
        public int NumberOfPins { get; set; }
    }
}
