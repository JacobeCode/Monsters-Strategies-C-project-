using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class BloodMageMonsterState : IState
    {
        private BloodMage bloodMage;
        private bool firstMove = true;

        public BloodMageMonsterState(BloodMage mage)
        {
            bloodMage = mage;
        }

        public List<StatPackage> GetNextAttack()
        {
            if (firstMove == true)
            {
                firstMove = false;
                int chargeSpeed = Index.RNG(1, 3);
                return new List<StatPackage>() { new StatPackage(DmgType.Other, chargeSpeed * Convert.ToInt32(15 + 0.2 * bloodMage.Strength), "Stwór rzuca się na Ciebie z wielką prędkością.") };
            }
            else
            {
                int howManyHits = Index.RNG(0, 2);
                return new List<StatPackage>() { new StatPackage(DmgType.Other, howManyHits * Convert.ToInt32(10 + 0.2 * bloodMage.Strength), "Stwór w szale okłada Cię swoimi odnóżami.") };
            }
        }

        public List<StatPackage> GetReact(List<StatPackage> enemyMove)
        {
            List<StatPackage> respond = new List<StatPackage>();
            foreach (StatPackage element in enemyMove)
            {
                bloodMage.Strength -= element.StrengthDmg / 2;
                bloodMage.Armor -= element.ArmorDmg / 2;
                bloodMage.Precision -= element.PrecisionDmg / 2;
                bloodMage.MagicPower -= element.MagicPowerDmg / 2;
                bloodMage.Health -= element.HealthDmg / 2;
                respond.Add(element);
            }
            return respond;
        }
    }
}
