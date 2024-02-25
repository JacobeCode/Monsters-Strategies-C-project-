using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters;

namespace Game.Engine.Monsters
{
	[Serializable]
	class EyeOfDoom : Monster
	{ 
		private IState currentState;

		public EyeOfDoom()
		{
			Health = 300;
			Strength = 30;
			Armor = 10;
			Precision = 40;
			MagicPower = 50;
			Stamina = 200;
			XPValue = 200;
			Name = "monster0007";
			BattleGreetings = "Jestem tu, by Cię zabić ... robaku.";
		}

		public void ChangeState(IState whatState)
        {
			currentState = whatState;
        }
		public override List<StatPackage> BattleMove()
        {
			return currentState.GetNextAttack();
		}

		public override List<StatPackage> React(List<StatPackage> packs)
        {
			return currentState.GetReact(packs);
		}
	}
}
