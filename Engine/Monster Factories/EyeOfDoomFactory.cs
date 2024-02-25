using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters;

namespace Game.Engine.Monsters.MonsterFactories
{
	[Serializable]
	class EyeOfDoomFactory : MonsterFactory
	{
        public override Monster Create()
        {
            EyeOfDoom eye = new EyeOfDoom();
            EyeOfDoomStartState startEye = new EyeOfDoomStartState(eye);
            eye.ChangeState(startEye);
            return eye;
        }
        public override System.Windows.Controls.Image Hint()
        {
            return new EyeOfDoom().GetImage();
        }
    }
}