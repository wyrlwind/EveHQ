using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities.Killmail
{
    public sealed class Victim
    {
        public uint CharacterID { get; set; }
        public string CharacterName { get; set; }
        public uint CorporationID { get; set; }
        public string CorporationName { get; set; }
        public uint AllianceID { get; set; }
        public string AllianceName { get; set; }
        public uint FactionID { get; set; }
        public string FactionName { get; set; }
        public uint DamageTaken { get; set; }
        public uint ShipTypeID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
}