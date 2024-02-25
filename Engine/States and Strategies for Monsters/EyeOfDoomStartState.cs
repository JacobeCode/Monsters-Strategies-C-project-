using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class EyeOfDoomStartState : IState
    {
        private EyeOfDoom eyeOfDoom;
        private bool isAttacksPhysical = false;
        private bool isAttacksMagical = false;
        private bool shieldIsActive = false;
        public EyeOfDoomStartState(EyeOfDoom whatEye)
        {
            eyeOfDoom = whatEye;
        }
        public void ChangeState(IState whatState)
        {
            eyeOfDoom.ChangeState(whatState);
        }
        public List<StatPackage> GetNextAttack()
        {
            if (eyeOfDoom.Health < 150)
            {
                ChangeState(new EyeOfDoomRageState(eyeOfDoom));
                eyeOfDoom.Strength -= 30;
                eyeOfDoom.MagicPower += 30;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Oko wścieka się. Obaszar walki wypełnia dziwna aura.") };
            }
            if (eyeOfDoom.Stamina > 40)
            {
                if (isAttacksPhysical == true)
                {
                    return new EyeOfDoomStunAttack().GetNextMove(eyeOfDoom);
                }
                if (isAttacksMagical == true)
                {
                    eyeOfDoom.Stamina -= 20;
                    if (eyeOfDoom.Health > 225)
                    {
                        shieldIsActive = false;
                        return new List<StatPackage>() { new StatPackage(DmgType.Fire, Convert.ToInt32(15 + 0.2 * eyeOfDoom.MagicPower), "Oko strzela w Ciebie słupem ognia ze swojego .... oka ?  ( " + Convert.ToInt32(15 + 0.2 * eyeOfDoom.MagicPower) + " dmg [ogień] )") };
                    }
                    else
                    {
                        shieldIsActive = true;
                        return new List<StatPackage>() { new StatPackage(DmgType.Physical, Convert.ToInt32(10 + 0.2 * eyeOfDoom.Strength), "Oko tworzy tarczę antymagiczną, jednocześnie atakując. Utrzymanie tarczy męczy oko, co osłabia jego atak.  ( " + Convert.ToInt32(10 + 0.2 * eyeOfDoom.Strength) + " dmg [fizyczne] )") };
                    }
                }
                return new List<StatPackage>() { new StatPackage(DmgType.Physical, Convert.ToInt32(20 + 0.2 * eyeOfDoom.Strength), "Oko atakuje Cię swoimi elektrycznymi mackami ( " + Convert.ToInt32(20 + 0.2 * eyeOfDoom.Strength) + " dmg [fizyczne] )") };
            }
            else
            {
                eyeOfDoom.Stamina += 20;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Oko jest zmęczone musi odpocząć.")};
            }
        }

        public List<StatPackage> GetReact(List<StatPackage> enemyMove)
        {
            List<StatPackage> respond = new List<StatPackage>();
            foreach (StatPackage element in enemyMove)
            {
                eyeOfDoom.Strength -= element.StrengthDmg;
                eyeOfDoom.Armor -= element.ArmorDmg;
                eyeOfDoom.Precision -= element.PrecisionDmg;
                if (DmgTest.Magic(element.DamageType) == true)
                {
                    isAttacksMagical = true;
                }
                if (DmgTest.Physical(element.DamageType) == true)
                {
                    isAttacksPhysical = true;
                }
                if (shieldIsActive == true)
                {
                    eyeOfDoom.Health -= (element.HealthDmg) / 2;
                    eyeOfDoom.MagicPower -= element.MagicPowerDmg / 2;
                }
                else if (DmgTest.Physical(element.DamageType) == true)
                {
                    eyeOfDoom.Health -= Convert.ToInt32(element.HealthDmg - 0.5 * eyeOfDoom.Armor);
                }
                else
                {
                    eyeOfDoom.Health -= (element.HealthDmg);
                    eyeOfDoom.MagicPower -= element.MagicPowerDmg;
                }
                respond.Add(element);
            }
            return respond;
        }
    }
}
