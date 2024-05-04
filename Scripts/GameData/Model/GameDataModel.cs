using System;
using System.Collections.Generic;

namespace Dobeil
{
	[Serializable]
	public class PlayerProfile
	{
		public int level;
		public PuzzleLevelData lastPuzzleState;
	}
}