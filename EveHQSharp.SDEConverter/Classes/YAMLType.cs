using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveHQSharp.SDEConverter
{
    class YAMLType
    {
        public int typeId;
        public int iconId;
        public String iconName;
        public IDictionary<int, List<int>> masteries;
        public IDictionary<int, List<String>> traits;
    }
}
