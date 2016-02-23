using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EveHQ.NewEveApi.Entities.Killmail
{
    public sealed class KillMail
    {
        public uint KillID { get; set; }
        public uint SolarSystemID { get; set; }
        public DateTimeOffset KillTime { get; set; }
        public uint MoonID { get; set; }
        public Victim Victim { get; set; }
        public IEnumerable<Attacker> Attackers { get; set; }
        public IEnumerable<KillItem> Items { get; set; }
    }
}