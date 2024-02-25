using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters;

namespace Game.Engine.Monsters.MonsterFactories
{
    [Serializable]
    class MindEaterFactory : MonsterFactory
    {
        public override Monster Create()
        {
            MindEater mind = new MindEater();
            MindEaterStartState startMind = new MindEaterStartState(mind);
            mind.ChangeState(startMind);
            return mind;
        }
        public override System.Windows.Controls.Image Hint()
        {
            return new MindEater().GetImage();
        }
    }
}