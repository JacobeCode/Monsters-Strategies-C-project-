using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class MindEaterStartState : IState
    {
        private MindEater mindEater;
        private bool secondAttack = false;

        public MindEaterStartState(MindEater whatMind)
        {
            mindEater = whatMind;
        }

        public void ChangeState(IState whatState)
        {
            mindEater.ChangeState(whatState);
        }
        public List<StatPackage> GetNextAttack()
        {
            mindEater.Strength += mindEater.PlayerValues["Strength"] / 2;
            mindEater.Armor += mindEater.PlayerValues["Armor"] / 2;
            mindEater.Precision += mindEater.PlayerValues["Precision"] / 2;
            mindEater.MagicPower += mindEater.PlayerValues["MagicPower"] / 2;
            return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, mindEater.PlayerValues["Strength"] / 4, mindEater.PlayerValues["Armor"] / 4, mindEater.PlayerValues["Precision"] / 4, mindEater.PlayerValues["MagicPower"] / 4, "Dziwna fala energii przepływa od Ciebie do stwora ...") };
        }

        public List<StatPackage> GetReact(List<StatPackage> enemyMove)
        {
            List<StatPackage> respond = new List<StatPackage>();
            foreach (StatPackage element in enemyMove)
            {
                mindEater.Strength -= element.StrengthDmg;
                mindEater.Armor -= element.ArmorDmg;
                mindEater.Precision -= element.PrecisionDmg;
                mindEater.MagicPower -= element.MagicPowerDmg;
                mindEater.Health -= element.HealthDmg;
                if (secondAttack == true && DmgTest.Physical(element.DamageType) == true)
                {                  
                    ChangeState(new MindEaterMagicState(mindEater));
                }
                else if (secondAttack == true && DmgTest.Magic(element.DamageType) == true)
                {
                    ChangeState(new MindEaterPhysicalState(mindEater));
                }
                secondAttack = true;
                respond.Add(element);
            }
            return respond;
        }
    }
}
