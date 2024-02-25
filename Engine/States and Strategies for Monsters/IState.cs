using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters
{
    interface IState
    {
        List<StatPackage> GetNextAttack();
        List<StatPackage> GetReact(List<StatPackage> enemyMove);
    }
}
