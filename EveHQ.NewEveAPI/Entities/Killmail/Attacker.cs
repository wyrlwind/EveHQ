using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities.Killmail
{
    public sealed class Attacker
    {
        public uint CharacterID { get; set; }
        public string CharacterName { get; set; }
        public uint CorporationID { get; set; }
        public string CorporationName { get; set; }
        public uint AllianceID { get; set; }
        public string AllianceName { get; set; }
        public uint FactionID { get; set; }
        public string FactionName { get; set; }
        public double SecurityStatus { get; set; }
        public uint DamageDone { get; set; }
        public bool FinalBlow { get; set; }
        public uint WeaponTypeID { get; set; }
        public uint ShipTypeID { get; set; }
    }
}