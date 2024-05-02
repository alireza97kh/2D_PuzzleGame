using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dobeil
{
	[System.Serializable]
	public class PuzzleLevelSpirteData
	{
		public int row;
		public int col;
		public string puzzleImageFilePath;
		//public byte[] pngBytes;
	}
	[System.Serializable]
	public class PuzzleLevelData
	{
		public int level;
		public List<PuzzleLevelSpirteData> levelData = new List<PuzzleLevelSpirteData>();
	}
	[System.Serializable]
	public class Puzzles
	{
		public string fullImagePath;
		public List<PuzzleLevelData> puzzleLevel = new List<PuzzleLevelData>();
	}
}