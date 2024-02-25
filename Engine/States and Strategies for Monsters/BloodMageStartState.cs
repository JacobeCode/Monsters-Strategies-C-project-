using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class BloodMageStartState : IState
    {
        private BloodMage bloodMage;
        private bool constantDamage = false;
        private StatPackage constantDamagePack = new StatPackage(DmgType.Poison, 20, "Krąg zadaje Ci kolejne obrażenia.");

        public BloodMageStartState(BloodMage mage)
        {
            bloodMage = mage;
        }
        public List<StatPackage> GetNextAttack()
        {
            int whichAttack = Index.RNG(1, 3);
            if (bloodMage.Health >= 200)
            {
                if (bloodMage.Stamina >= 60 && constantDamage == false)
                {
                    bloodMage.Stamina -= 60;
                    bloodMage.Health -= 20;
                    constantDamage = true;
                    return new List<StatPackage>() { new StatPackage(DmgType.Physical, Convert.ToInt32(20 + 0.2 * bloodMage.MagicPower), "Wokól miejsca walki mag tworzy krąg. Rzuca Cię to w pobliskie skały. Najwyraźniej krąg wyciąga z Ciebie energię. (" + Convert.ToInt32(20 + 0.2 * bloodMage.MagicPower) + " dmg [fizyczne])") };
                }
                else if (whichAttack == 1 && bloodMage.Stamina >= 30 && constantDamage == true)
                {
                    bloodMage.Stamina -= 30;
                    return new List<StatPackage>() { new StatPackage(DmgType.Poison, Convert.ToInt32(15 + 0.2 * bloodMage.MagicPower), 0, 0, 0, 0, "Mag rzuca na Ciebie trujące pociski. (" + Convert.ToInt32(15 + 0.2 * bloodMage.MagicPower) + " dmg [trujące])"), constantDamagePack};
                }
                else if (whichAttack == 1 && bloodMage.Stamina >= 30)
                {
                    bloodMage.Stamina -= 30;
                    return new List<StatPackage>() { new StatPackage(DmgType.Poison, Convert.ToInt32(15 + 0.2 * bloodMage.MagicPower), 0, 0, 0, 0, "Mag rzuca na Ciebie trujące pociski. (" + Convert.ToInt32(15 + 0.2 * bloodMage.MagicPower) + " dmg [trujące])") };
                }
                else if (whichAttack == 2 && bloodMage.Stamina >= 30 && constantDamage == true)
                {
                    bloodMage.Stamina -= 30;
                    return new List<StatPackage>() { new StatPackage(DmgType.Other, Convert.ToInt32(5 + 0.2 * bloodMage.MagicPower), 2, 2, 2, 2, "Mag rzuca się na Ciebie z niewiarygodną szybkością, łapiąc Cię za głowę i wciągając Twoją energię. (" + Convert.ToInt32(5 + 0.2 * bloodMage.MagicPower) + " dmg [trujące], [ -2 do wszystkich statystyk ])"), constantDamagePack };
                }
                else if (whichAttack == 2 && bloodMage.Stamina >= 30)
                {
                    bloodMage.Stamina -= 30;
                    return new List<StatPackage>() { new StatPackage(DmgType.Poison, Convert.ToInt32(5 + 0.2 * bloodMage.MagicPower), 2, 2, 2, 2, "Mag rzuca się na Ciebie z niewiarygodną szybkością, łapiąc Cię za głowę i wciągając Twoją energię. (" + Convert.ToInt32(5 + 0.2 * bloodMage.MagicPower) + " dmg [trujące], [ -2 do wszystkich statystyk ])") };
                }
                else if (whichAttack == 3 && bloodMage.Stamina >= 50)
                {
                    bloodMage.Stamina -= 50;
                    if ( constantDamage == true)
                    {
                        return new List<StatPackage>() { new StatPackage(DmgType.Poison, Convert.ToInt32(25 + 0.2 * bloodMage.MagicPower), 0, 10, 0, 0, "Mag wspomagany stworzonym kręgiem wypuszcza trujący promień. (" + Convert.ToInt32(25 + 0.2 * bloodMage.MagicPower) + " dmg [trujące], [ -10 precyzja ])") };
                    }
                    else
                    {
                        return new List<StatPackage>() { new StatPackage(DmgType.Poison, Convert.ToInt32(25 + 0.2 * bloodMage.MagicPower), 0, 5, 0, 0, "Mag wypuszcza trujący promień. (" + Convert.ToInt32(25 + 0.2 * bloodMage.MagicPower) + " dmg [trujące], [ -5 precyzja ])") };
                    }

                }
                else if (constantDamage == true)
                {
                    bloodMage.Stamina += 20;
                    return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mag jest zmęczony, musi odpocząć."), constantDamagePack };
                }
                else
                {
                    bloodMage.Stamina += 30;
                    return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mag jest zmęczony, musi odpocząć.")};
                }
            }
            else if(bloodMage.Health > 100)
            {
                bloodMage.ChangeState(new BloodMageCastState(bloodMage));
                if (bloodMage.Stamina >= 20)
                {
                    bloodMage.Stamina -= 20;
                    if (constantDamage == true)
                    {
                        bloodMage.Health += 10;
                        return new List<StatPackage>() { new StatPackage(DmgType.Other, Convert.ToInt32(25 + 0.2 * bloodMage.MagicPower), "Mag wyrzuca pocisk, który lekko Cię drasnął. Widzisz, że zwiększył dystans niszcząc i pochłaniając stworzony wcześniej krąg, a jego oczy zaszły czerwienią. (" + Convert.ToInt32(25 + 0.2 * bloodMage.MagicPower) + " dmg [magiczne]) ") };
                    }
                    else
                    {
                        return new List<StatPackage>() { new StatPackage(DmgType.Other, Convert.ToInt32(25 + 0.2 * bloodMage.MagicPower), "Mag wyrzuca pocisk, który lekko Cię drasnął. Jednak widzisz, że zwiększył dystans, a jego oczy zaszły czerwienią. ( " + Convert.ToInt32(25 + 0.2 * bloodMage.MagicPower) + " dmg [magiczne]) ") };
                    }
                }
                else
                {
                    if (constantDamage == true)
                    {
                        bloodMage.Health += 10;
                        return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mag zwiększa dystans niszcząc i pochłaniając stworzony wcześniej krąg. Widzisz, że jego oczy zachodzą czerwienią.") };
                    }
                    else
                    {
                        return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Mag zwiększa dystans. Widzisz, że jego oczy zachodzą czerwienią.") };
                    }
                }
            }
            else
            {
                bloodMage.ChangeState(new BloodMageMonsterState(bloodMage));
                if (constantDamage == true)
                {
                    bloodMage.Health += 30;
                    bloodMage.Strength += 10;
                    return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Widzisz jak mag traci nad sobą kontrolę, a czerwone macki pochłaniają jego nogi i ręce tworząc straszny byt, chcący jedynie niszczyć, a wcześniej stworzony krąg wzmacnia go.") };
                }
                else
                {
                    bloodMage.Health += 20;
                    return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Widzisz jak mag traci nad sobą kontrolę, a czerwone macki pochłaniają jego nogi i ręce tworząc straszny byt, chcący jedynie niszczyć") };
                }
            }
        }
        public List<StatPackage> GetReact(List<StatPackage> enemyMove)
        {
            List<StatPackage> respond = new List<StatPackage>();
            foreach (StatPackage element in enemyMove)
            {
                bloodMage.Strength -= element.StrengthDmg;
                bloodMage.Armor -= element.ArmorDmg;
                bloodMage.Precision -= element.PrecisionDmg;
                int dodgeChance = Index.RNG(0, 10);
                bloodMage.MagicPower -= (dodgeChance / 10 * element.MagicPowerDmg);
                bloodMage.Health -= (dodgeChance / 10 * element.HealthDmg);
                respond.Add(element);
            }
            return respond;
        }
    }
}
