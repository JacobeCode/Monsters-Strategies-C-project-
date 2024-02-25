using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    class EyeOfDoomRageState : IState
    {
        private EyeOfDoom eyeOfDoom;
        private bool finish = false;
        private bool firstMove = true;
        private string finishMessage = "";
        public EyeOfDoomRageState(EyeOfDoom whatEye)
        {
            eyeOfDoom = whatEye;
        }
        public void ChangeState(IState whatState)
        {
            eyeOfDoom.ChangeState(whatState);
        }
        public List<StatPackage> GetNextAttack()
        {
            if (firstMove == true)
            {
                firstMove = false;
                return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, 5, 5, 5, 10, "Oko rzuca się całkowicie w wir walki. Wyzwala potężną falę magii. ( [ -5 pancerz, -5 precyzja, -5 siła, -10 moc ] )") };
            }
            else
            {
                if (eyeOfDoom.Health < 40)
                {
                    finish = true;
                    finishMessage = "Oko przeczuwa swoją porażkę. W akcie desperacji co pewnien czas teleportuje się w inne miejsce. Utrudnia Ci to trafienie.";
                }
                int whatAttack = Index.RNG(1, 4);
                switch (whatAttack)
                {
                    case 1:
                        eyeOfDoom.Stamina -= 10;
                        return new List<StatPackage>() { new StatPackage(DmgType.Fire, Convert.ToInt32(20 + (0.3 * eyeOfDoom.MagicPower)), "Oko atakuje Cię wzmocnionym słupem ognia. ( " + Convert.ToInt32(20 + (0.3 * eyeOfDoom.MagicPower)) + " dmg [ogień] )\n" + finishMessage) };
                    case 2:
                        eyeOfDoom.Stamina -= 10;
                        return new List<StatPackage>() { new StatPackage(DmgType.Psycho, Convert.ToInt32(0.1 * eyeOfDoom.MagicPower), 5, 0, 5, 0, "Z wnętrza oka dobiega przenikliwy krzyk, który sprawia, że czujesz się przytłoczony. ( " + Convert.ToInt32(0.1 * eyeOfDoom.MagicPower) + " dmg [psycho] , [ -5 precyzja, - 5 siła ] \n" + finishMessage) };
                    case 3:
                        eyeOfDoom.Stamina -= 30;
                        int howManyHits = Index.RNG(1, 4);
                        int brokenArm = Index.RNG(0, 1);
                        if (brokenArm == 1)
                        {
                            return new List<StatPackage>() { new StatPackage(DmgType.Earth, Convert.ToInt32((howManyHits * ((0.2 * eyeOfDoom.MagicPower)) + 10)), 5, 0, 5, 0, "Za Tobą materializują się kamienne szpile i mkną w Twoją stronę. Niestety jedna z nich przebija Twoją rękę i obniża sprawność, pozostałe zadają Ci trochę obrażeń. ( " + Convert.ToInt32((howManyHits * ((0.2 * eyeOfDoom.MagicPower)) + 10)) + " dmg [ziemia] , [ -5 siły, -5 precyzja ] )\n" + finishMessage) };
                        }
                        else
                        {
                            return new List<StatPackage>() { new StatPackage(DmgType.Earth, Convert.ToInt32((howManyHits * ((0.2 * eyeOfDoom.MagicPower)) + 10)), 0, 0, 0, 0, "Za Tobą materializują się kamienne szpile i mkną w Twoją stronę. Zadają Ci trochę obrażeń. (" + Convert.ToInt32((howManyHits * ((0.2 * eyeOfDoom.MagicPower)) + 10)) + " dmg [ziemia])\n" + finishMessage) };
                        }
                    case 4:
                        eyeOfDoom.Stamina -= 5;
                        eyeOfDoom.Health -= 25;
                        return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Wygląda na to, że masz szczęście, energia magiczna wymyka się spod kontroli oka i wybucha w pobliżu przeciwnika, raniąc go.\n" + finishMessage) };
                    default:
                        return new List<StatPackage>() { new StatPackage(DmgType.Other, 0, "Wygląda na to, że masz szczęście, na polu walki pojawiła się anomalia i odwróciła uwagę oka.\n" + finishMessage) };
                }
            }
        }
        public List<StatPackage> GetReact(List<StatPackage> enemyMove)
        {
            List<StatPackage> respond = new List<StatPackage>();
            foreach (StatPackage element in enemyMove)
            {
                eyeOfDoom.Strength -= element.StrengthDmg;
                eyeOfDoom.Armor -= element.ArmorDmg;
                eyeOfDoom.Precision -= element.PrecisionDmg;            //Eye in this state is have full resistance for magical debuffs
                if (finish == true)
                {
                    int chanceForHit = Index.RNG(0, 10);
                    eyeOfDoom.Health -= chanceForHit / 10 * (element.HealthDmg - 1 / 2 * eyeOfDoom.Armor);
                }
                else
                {
                    eyeOfDoom.Health -= (element.HealthDmg - 1 / 2 * eyeOfDoom.Armor);
                }
                respond.Add(element);
            }
            return respond;
        }
    }
}
