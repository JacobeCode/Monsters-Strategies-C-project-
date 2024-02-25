using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class BloodMageCastMagicStrategy : IStrategy
    {
        public List<StatPackage> GetNextMove(Monster whatMonster)
        {
            if (whatMonster.Health > 180)
            {
                int curseStrength = Index.RNG(0, 10);
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, 0, 0 , 0, curseStrength, "Mag rzuca klątwę obniżając Twoje zdolności magiczne [ -" + curseStrength + " siły magii ]") };
            }
            else if (whatMonster.Health > 150)
            {
                int howManyProjectiles = Index.RNG(0, 3);
                if (howManyProjectiles == 0)
                {
                    return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mag próbuje przywołać krwawe pociski, jednak nie udaje mu się.") };
                }
                else
                {
                    return new List<StatPackage>() { new StatPackage(DmgType.Other, howManyProjectiles * 15, "Mag tworzy " + howManyProjectiles + " krwawe pociski i rzuca w Twoją stronę. ( " + howManyProjectiles * 15 + "dmg [magiczne]) ") };
                }
            }
            else
            {
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, 5, 5, 5, 5, "Mag rzuca zaklęcie strachu, które paraliżuje Cię i obniża podstawowe statystyki.") };
            }
        }
    }
}
