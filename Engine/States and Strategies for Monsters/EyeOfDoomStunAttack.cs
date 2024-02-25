using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class EyeOfDoomStunAttack : IStrategy
    {
        public List<StatPackage> GetNextMove(Monster whatMonster)
        {
            int howManyTurns = Index.RNG(1, 3);
            if (howManyTurns == 1)
            {
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Oko próbuje na Tobie petryfikacji. Opierasz się jego działaniu. ( " + (0) + " dmg [magiczne] )") };
            }
            else
            {
                int howManyAttacks = Index.RNG(0, howManyTurns);
                int damageValue = Convert.ToInt32(howManyAttacks * (20 + 0.2 * whatMonster.Strength));
                int healValue = Convert.ToInt32((howManyTurns - howManyAttacks) * (15 + 0.1 * whatMonster.MagicPower));
                string damageMessage = "Dodatkowy atak ( " + (20 + 0.2 * whatMonster.Strength) + " dmg [fizyczne] )\n";
                string healMessage = "Leczenie ( " + (15 + 0.1 * whatMonster.MagicPower) + " pkt. zdrowia przeciwnika )\n";
                string wholeMessage = "";
                for (int iterator = howManyAttacks; iterator > 0; iterator--)
                {
                    wholeMessage += damageMessage;
                }
                for (int iterator = 2 - howManyAttacks; iterator > 0; iterator--)
                {
                    wholeMessage += healMessage;
                }
                whatMonster.Health += healValue;
                whatMonster.Stamina -= (howManyAttacks * 15) + ((howManyTurns - howManyAttacks) * 15);
                return new List<StatPackage>() { new StatPackage(DmgType.Other, damageValue, "Oko petryfikuje Cię.\nWykorzystuje tury na : \n" + wholeMessage) };
            }
        }
    }
}
