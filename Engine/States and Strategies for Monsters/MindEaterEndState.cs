using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class MindEaterEndState : IState
    {
        private MindEater mindEater;
        private bool firstMove = true;
        private bool destroyEverythingInThisParticularDirection = false;

        public MindEaterEndState(MindEater whatMind)
        {
            mindEater = whatMind;
        }

        public void ChangeState(IState whatState)
        {
            mindEater.ChangeState(whatState);
        }

        public List<StatPackage> GetNextAttack()
        {
            if (mindEater.Health < 150 && mindEater.PlayerValues["Health"] < 70)
            {
                if (firstMove == true)
                {
                    firstMove = false;
                    if (mindEater.Stamina > 40)
                    {
                        mindEater.Stamina -= 40;
                        if (mindEater.PlayerValues["Strength"] > 40)
                        {
                            return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, 0, 5, 0, 0, "Ledwie trzymasz się na nogach, ale Twój adwersarz nie jest w lepszym stanie. Rzuca się na Ciebie ze złowieszczym krzykiem próbując podciąć Cię ogonem. Udaje Ci się go złapać, dzięki czemu przesuwa Cię jedynie lekko obijając pancerz ([ -5 pancerz ])") };
                        }
                        else
                        {
                            int damageCalc = mindEater.CalcDamage(mindEater.PlayerValues["Strength"], mindEater.CalcDamage(mindEater.PlayerValues["Strength"], Convert.ToInt32(30 + (0.1 * mindEater.Strength)))); // Dla poziomu statystki 20 pkt. 100 % obrażeń - im większa siła tym mniejsze obrażenia - im mniejsza tym większe dodatkowe obrażenia
                            return new List<StatPackage>() { new StatPackage(DmgType.Physical, damageCalc, 0, 10, 0, 0, "Ledwie trzymasz się na nogach, ale Twój adwersarz nie jest w lepszym stanie. Rzuca się na Ciebie ze złowieszczym krzykiem próbując podciąć Cię ogonem. Rzuca Cię o ziemię i uszkadza pancerz. (" + (damageCalc) + " dmg [fizyczne] ) + [- 10 pancerz]") };
                        }
                    }
                    else
                    {
                        mindEater.Stamina += Index.RNG(1, 3) * 20;
                        return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Ledwie trzymasz się na nogach, ale Twój adwersarz nie jest w lepszym stanie. Pożeracz jednak nie jest w stanie się nawet ruszyć.") };
                    }
                }
                else if (mindEater.Stamina > 20)
                {
                    mindEater.Stamina -= 20;
                    if (mindEater.PlayerValues["Strength"] > 40)
                    {
                        return new List<StatPackage>() { new StatPackage(DmgType.Physical, 0, 0, 5, 0, 0, "Stwór nadal w furii rzuca się na Ciebie ze swoim ogonem obijając pancerz [ -5 pancerz ].") };
                    }
                    else
                    {
                        int damage = mindEater.CalcDamage(mindEater.PlayerValues["Strength"], mindEater.CalcDamage(mindEater.PlayerValues["Precision"], Convert.ToInt32(30 + (0.2 * mindEater.Strength))));
                        int armor = mindEater.CalcDamage(mindEater.PlayerValues["Strength"], mindEater.CalcDamage(mindEater.PlayerValues["Precision"], Convert.ToInt32(10)));
                        return new List<StatPackage>() { new StatPackage(DmgType.Physical, damage, 0, armor, 0, 0, "Stwór nadal w furii rzuca się na Ciebie ze swoim ogonem. (" + damage + "dmg [fizyczne] + [ - " + armor + " pancerza ] ).") };
                    }
                }
                else
                {
                    mindEater.Stamina += 30;
                    return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Pożeracz jest zmęczony.") };
                }
            }
            else if (mindEater.Health < 150)
            {
                if (firstMove == true)
                {
                    firstMove = false;
                    if (mindEater.Stamina > 30)
                    {
                        mindEater.Stamina -= 30;
                        if (mindEater.PlayerValues["Precision"] > 40)
                        {
                            return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Pożeracz jest na skraju wytrzymałości, dlatego wycofuje się i skupia na atakach dystansowych rzucając w Ciebie ognistą kulę. Unikasz jej prostym zwodem.") };
                        }
                        else
                        {
                            int damageCalc = mindEater.CalcDamage(mindEater.PlayerValues["Precision"], Convert.ToInt32(20 + (0.2 * mindEater.MagicPower))); // Dla poziomu statystki 20 pkt. 100 % obrażeń - im większa precyzja tym mniejsze obrażenia - im mniejsza tym większe dodatkowe obrażenia
                            return new List<StatPackage>() { new StatPackage(DmgType.Physical, damageCalc, "Pożeracz jest na skraju wytrzymałości, dlatego wycofuje się i skupia na atakach dystansowych rzucając w Ciebie ognistą kulę. ( " + (damageCalc) + " dmg [magiczne] )") };
                        }
                    }
                    else
                    {
                        mindEater.Stamina += Index.RNG(1, 3) * 20;
                        return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Pożeracz jest na skraju wytrzymałości, nie ma siły nawet na atak, dlatego wycofuje się w tył.") };
                    }
                }
                else
                {
                    if (mindEater.PlayerValues["Precision"] > 50 || mindEater.PlayerValues["MagicPower"] > 50 && mindEater.Stamina > 20)
                    {
                        if (mindEater.PlayerValues["Precision"] > mindEater.PlayerValues["MagicPower"])
                        {
                            mindEater.Stamina -= 20;
                            return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Puszczane kule ognia są przez Ciebie łatwo unikane.") };
                        }
                        else
                        {
                            mindEater.Stamina -= 20;
                            return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Puszczane kule ognia są przez Ciebie łatwo odbijane magiczną aurą") };
                        }
                    }
                    else if (mindEater.Stamina > 20)
                    {
                        mindEater.Stamina -= 20;
                        int damage = mindEater.CalcDamage(mindEater.PlayerValues["Precision"], Convert.ToInt32(15 + (0.2 * mindEater.Precision))) + mindEater.CalcDamage(mindEater.PlayerValues["MagicPower"], Convert.ToInt32(15 + (0.2 * mindEater.MagicPower)));
                        return new List<StatPackage>() { new StatPackage(DmgType.Fire, damage, "Pożeracz miota w Ciebie rozpaczliwie kule ognia. ( " + (damage) + "dmg [magiczne] )" ) };
                    }
                    else
                    {
                        mindEater.Stamina += 30;
                        return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Pożeracz jest zmęczony.") };
                    }
                }
            }
            else if (mindEater.PlayerValues["Health"] < 70 || destroyEverythingInThisParticularDirection == true)
            {
                if (firstMove == true)
                {
                    firstMove = false;
                    destroyEverythingInThisParticularDirection = true;
                    mindEater.Stamina += 60;
                    return new List<StatPackage>() { new StatPackage(DmgType.Fire, 0, "Słyszysz przerażający śmiech zwiastujący koniec Twojej przygody. Widzisz wielką kulę ognia pojawiającą się w koronie pożeracza. Ładuje ją.") };
                }
                else
                {
                    destroyEverythingInThisParticularDirection = false;
                    mindEater.Stamina -= 60;
                    int damageCalc = mindEater.CalcDamage(mindEater.PlayerValues["MagicPower"], 100);
                    return new List<StatPackage>() { new StatPackage(DmgType.Fire, damageCalc, "Śmiech kończy się przeraźliwym skrzekiem, który jednocześnie uwalnia kulę w koronie stwora. ( " + (damageCalc) +"dmg [magiczne]") };
                }
            }                     
            mindEater.Health -= 10;
            return new List<StatPackage>() { new StatPackage(DmgType.Physical, 10, 0, 0, 0, 0, "Na polu pojawia się dziwna anomalia i rani Ciebie oraz Twojego przeciwnika.") };
        }

        public List<StatPackage> GetReact(List<StatPackage> enemyMove)
        {
            List<StatPackage> respond = new List<StatPackage>();
            foreach (StatPackage element in enemyMove)
            {
                mindEater.Strength -= element.StrengthDmg / 2;
                mindEater.Armor -= element.ArmorDmg / 2;
                mindEater.Precision -= element.PrecisionDmg / 2;
                mindEater.MagicPower -= element.MagicPowerDmg / 2;
                mindEater.Health -= element.HealthDmg / 2;
                respond.Add(element);
            }
            return respond;
        }
    }
}
