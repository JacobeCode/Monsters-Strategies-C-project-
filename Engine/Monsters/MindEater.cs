using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Monsters.Built_In.States_and_Strategies_for_Monsters;

namespace Game.Engine.Monsters
{
	[Serializable]
	class MindEater : Monster
	{
		private IState currentState;

		public MindEater() // ze względu, że ten przypadek uwzględnia statystyki gracza przyjmuję, że stat Precision jest niejako odpowiednikiem klasycznej zręczności z gier RPG
		{
			Health = 300;
			Strength = 30;
			Armor = 10;
			Precision = 40;
			MagicPower = 30;
			Stamina = 200;
			XPValue = 250;
			Name = "monster0008";
			BattleGreetings = "Widzę Cię ...";
		}

		public int CalcDamage(int stat, int attackDamage)
        {
			return Convert.ToInt32(attackDamage - (0.2 * stat));
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
