using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class BloodMageCastState : IState
    {
        private BloodMage bloodMage;
        private IStrategy currentStrategy;
        private bool firstMove = true;
        private bool IsAttackPhysical = false;
        private bool IsAttackMagic = false;

        public BloodMageCastState(BloodMage mage)
        {
            bloodMage = mage;
        }

        public List<StatPackage> GetNextAttack()
        {
            if (bloodMage.Health < 100)
            {
                bloodMage.ChangeState(new BloodMageMonsterState(bloodMage));
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Widzisz jak mag traci nad sobą kontrolę, a czerwone macki pochłaniają jego nogi i ręce tworząc straszny byt, chcący jedynie niszczyć.") };
            }
            else if (firstMove == true)
            {
                firstMove = false;
                bloodMage.Health -= 10;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mag rozcina swoją rękę sztyletem. Wyciaga strugę krwi i tworzy wokół siebie tarczę.") };
            }
            else if (IsAttackPhysical == true)
            {
                currentStrategy = new BloodMageCastPhysicalStrategy();
            }
            else if (IsAttackMagic == true)
            {
                currentStrategy = new BloodMageCastMagicStrategy();
            }
            if (bloodMage.Stamina > 20)
            {
                bloodMage.Stamina -= 20;
                bloodMage.Health -= 5;
                return currentStrategy.GetNextMove(bloodMage);
            }
            else
            {
                bloodMage.Stamina += 30;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mag jest zmęczony, musi odpocząć") };
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
                if (DmgTest.Physical(element.DamageType) == true)
                {
                    IsAttackPhysical = true;
                    IsAttackMagic = false;
                }
                else if (DmgTest.Magic(element.DamageType) == true)
                {
                    IsAttackPhysical = false;
                    IsAttackMagic = true;
                }
                respond.Add(element);
            }
            return respond;
        }
    }
}
