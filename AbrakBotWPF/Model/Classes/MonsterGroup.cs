using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrakBotWPF.Model.Classes
{
    public class MonsterGroup
    {
        public int caseGroupe;
        public int level;
        public int id;
        public int nbMonstres;

        public MonsterGroup(int caseGroupe, int level, int id, int nbMonstres)
        {
            this.caseGroupe = caseGroupe;
            this.level = level;
            this.id = id;
            this.nbMonstres = nbMonstres;
        }
    }
}
