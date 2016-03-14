using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveHQ.EveCrest.Models.Crest
{
    public class IndustrySystem
    {
        public SolarSystem solarSystem { get; set; }
        public IEnumerable<SystemCostIndice> systemCostIndices { get; set; }
        
        public SystemCostIndice getSystemCostIndice(FactoryActivity activity)
        {
            int activityID = (int)activity;
            foreach (var costIndice in systemCostIndices)
            {
                if (costIndice.activityID == activityID)
                    return costIndice;
            }

            return null;
        }
    }
}
