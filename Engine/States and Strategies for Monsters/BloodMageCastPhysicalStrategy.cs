using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class BloodMageCastPhysicalStrategy : IStrategy
    {
        public List<StatPackage> GetNextMove(Monster whatMonster)
        {
            if(whatMonster.Health > 180)
            {
                int curseStrength = Index.RNG(0, 10);
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, curseStrength, curseStrength, curseStrength, 0, "Mag rzuca klątwę obniżając Twoje zdolności walki wręcz [ -" + curseStrength + " precyzja, siła, pancerz ]") };
            }
            else if(whatMonster.Health > 150)
            {
                int fireSword = Index.RNG(0, 2);
                int handHit = Index.RNG(0, 1);
                if(fireSword == 0)
                {
                    return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mag próbuje przywołać ognisty miecz, jednak nie udaje mu się.") };
                }
                else
                {
                    if (handHit == 0)
                    {
                        return new List<StatPackage>() { new StatPackage(DmgType.Other, fireSword * 15, "Mag przywołuje ognisty miecz. Udaje mu się trafić Cię " + fireSword + " razy. ( " + fireSword * 15 + "dmg [magiczne])") };
                    }
                    else
                    {
                        return new List<StatPackage>() { new StatPackage(DmgType.Other, fireSword * 15, 5, 0, 0, 0, "Mag przywołuje ognisty miecz. Udaje mu się trafić Cię " + fireSword + " razy. Dodatkowo trafia w rękę i okalecza Cię. ( " + fireSword * 15 + "dmg [magiczne] + [ - 5 siły ] )") };
                    }
                }
            }
            else
            {
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 10, "Mag przywołuje ognistą włócznię i zadaje Ci cios.") };
            }
        }
    }
}
