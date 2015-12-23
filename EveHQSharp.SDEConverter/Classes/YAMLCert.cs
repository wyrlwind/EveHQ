using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveHQSharp.SDEConverter
{
    class YAMLCert
    {
        public int certId;
        public String description;
        public int groupId;
        public String name;
        public IEnumerable<int> recommandedFor;
        public IEnumerable<CertReqSkill> requiredSkills;
    }

    class CertReqSkill
    {
        public int skillId;
        public IDictionary<String, int> skillLevels;
    }
}
