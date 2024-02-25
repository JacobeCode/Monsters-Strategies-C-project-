using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters;

namespace Game.Engine.Monsters.MonsterFactories
{
    [Serializable]
    class BloodMageFactory : MonsterFactory
    {
        public override Monster Create()
        {
            BloodMage bloodMage = new BloodMage();
            BloodMageStartState startMage = new BloodMageStartState(bloodMage);
            bloodMage.ChangeState(startMage);
            return bloodMage;
        }
        public override System.Windows.Controls.Image Hint()
        {
            return new BloodMage().GetImage();
        }
    }
}