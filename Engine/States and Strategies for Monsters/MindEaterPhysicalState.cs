using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class MindEaterPhysicalState : IState
    {
        private MindEater mindEater;

        public MindEaterPhysicalState(MindEater whatMind)
        {
            mindEater = whatMind;
        }

        public void ChangeState(IState whatState)
        {
            mindEater.ChangeState(whatState);
        }

        public List<StatPackage> GetNextAttack()
        {
            if (mindEater.Stamina > 50)
            {
                mindEater.Stamina -= 50;
                if (mindEater.PlayerValues["Strength"] > 40)
                {
                    mindEater.Health -= 10;
                    return new List<StatPackage>() { new StatPackage(DmgType.Physical, 0, "Pożeracz czuje się w pełni sił. Atakuje Cię zawzięcie swoim ogonem. Jednak dzięki swojej sile łapiesz go i tniesz powierzchownie swoją bronią, zadając niewielkie obrażenia. ( [ -10 dmg przeciwnika ] )") };
                }
                else
                {
                    int damageCalc = mindEater.CalcDamage(mindEater.PlayerValues["Strength"], Convert.ToInt32(30 + (0.2 * mindEater.Strength))); // Dla poziomu statystki 20 pkt. 100 % obrażeń - im większa siła tym mniejsze obrażenia - im mniejsza tym większe dodatkowe obrażenia
                    return new List<StatPackage>() { new StatPackage(DmgType.Physical, damageCalc, "Pożeracz czuje się w pełni sił. Atakuje Cię zawzięcie swoim ogonem ( " + (damageCalc) + " dmg [fizyczne] )") };
                }
            }
            else if (mindEater.Stamina > 30)
            {
                mindEater.Stamina -= 30;
                if (mindEater.PlayerValues["MagicPower"] > 40)
                {
                    return new List<StatPackage>() { new StatPackage(DmgType.Psycho, 0, "Pożeracz czuje się zmęczony. Próbuje więc umysłowej manipulacji. Jednak dzięki swojej magicznej wiedzy i odporności udało Ci się wyrzucić macki pożeracza ze swojej głowy.") };
                }
                else
                {
                    int damageCalc = mindEater.CalcDamage(mindEater.PlayerValues["Strength"], Convert.ToInt32(20 + (0.2 * mindEater.Strength)));                    // Dla poziomu statystki 20 pkt. 100 % obrażeń - im większa siła tym mniejsze obrażenia - im mniejsza tym większe dodatkowe obrażenia
                    return new List<StatPackage>() { new StatPackage(DmgType.Physical, damageCalc, "Pożeracz czuje się zmęczony. Próbuje więc umysłowej manipulacji. Niestety udaje mu się i zmusza Cię do ... uderzenia się we własną głowę. ( " + (damageCalc) + " dmg [fizyczne] )") };
                }
            }
            else
            {
                mindEater.Stamina += Index.RNG(1,3) * 30;
                return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Pożeracz czuje się zmęczony. Musi odpocząć.") };
            }
        }

        public List<StatPackage> GetReact(List<StatPackage> enemyMove)
        {
            List<StatPackage> respond = new List<StatPackage>();
            foreach (StatPackage element in enemyMove)
            {
                mindEater.Strength -= element.StrengthDmg;
                mindEater.Armor -= element.ArmorDmg;
                mindEater.Precision -= element.PrecisionDmg / 3;
                mindEater.MagicPower -= element.MagicPowerDmg / 3;
                mindEater.Health -= element.HealthDmg / 2;
                if (DmgTest.Physical(element.DamageType) == true)
                {
                    ChangeState(new MindEaterMagicState(mindEater));
                }
                else if (mindEater.Health < 150 || mindEater.PlayerValues["Health"] < 70)
                {
                    ChangeState(new MindEaterEndState(mindEater));
                }
                respond.Add(element);
            }
            return respond;
        }
    }
}