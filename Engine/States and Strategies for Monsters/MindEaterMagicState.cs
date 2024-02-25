using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class MindEaterMagicState : IState
    {
        private MindEater mindEater;

        public MindEaterMagicState(MindEater whatMind)
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
                if (mindEater.PlayerValues["Precision"] > 50)
                {
                    return new List<StatPackage>() { new StatPackage(DmgType.Earth, 0, "Pożeracz czuje się w pełni sił. Atakuje Cię zawzięcie, wzniecając trzęsienie ziemii. Jednak dzięki swojej zręczności unikasz wszelkich rozpadlin i spadających kamieni.") };
                }
                else
                {
                    int damageCalc = mindEater.CalcDamage(mindEater.PlayerValues["Precision"], Convert.ToInt32(30 + (0.2 * mindEater.Precision)));                    // Dla poziomu statystki 20 pkt. 100 % obrażeń - im większa precyzja tym mniejsze obrażenia - im mniejsza tym większe dodatkowe obrażenia
                    return new List<StatPackage>() { new StatPackage(DmgType.Earth, damageCalc, "Pożeracz czuje się w pełni sił. Atakuje Cię zawzięcie, wzniecając trzęsienie ziemii ( " + (damageCalc) + " dmg [ziemia]) ") };
                }
            }
            else if (mindEater.Stamina > 30)
            {
                mindEater.Stamina -= 30;
                if (mindEater.PlayerValues["MagicPower"] > 50)
                {
                    return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Pożeracz czuje się zmęczony. Atakuje Cię jednak ognistymi pociskami. Jednak dzięki swojej magicznej wiedzy zdołałeś stworzyć chroniącą Cię tarczę.") };
                }
                else
                {
                    int damageCalc = mindEater.CalcDamage(mindEater.PlayerValues["MagicPower"], Convert.ToInt32(20 + (0.2 * mindEater.Strength)));                    // Dla poziomu statystki 20 pkt. 100 % obrażeń - im większy magicpower tym mniejsze obrażenia - im mniejsza tym większe dodatkowe obrażenia
                    return new List<StatPackage>() { new StatPackage(DmgType.Fire, damageCalc, "Pożeracz czuje się zmęczony. Atakuje Cię jednak ognistymi pociskami. ( " + (damageCalc) + " dmg [magiczne] )") };
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
                mindEater.Strength -= element.StrengthDmg / 3;
                mindEater.Armor -= element.ArmorDmg / 3;
                mindEater.Precision -= element.PrecisionDmg;
                mindEater.MagicPower -= element.MagicPowerDmg;
                mindEater.Health -= element.HealthDmg / 2;
                if (DmgTest.Magic(element.DamageType) == true)
                {
                    ChangeState(new MindEaterPhysicalState(mindEater));
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
