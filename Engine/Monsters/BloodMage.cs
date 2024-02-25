using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters;

namespace Game.Engine.Monsters
{
	[Serializable]
	class BloodMage : Monster
	{
		private IState currentState;

		public BloodMage()
		{
			Health = 300;
			Strength = 30;
			Armor = 10;
			Precision = 40;
			MagicPower = 30;
			Stamina = 300;
			XPValue = 250;
			Name = "monster0009";
			BattleGreetings = "...";
		}
		
		public void ChangeState(IState state)
        {
			currentState = state;
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
