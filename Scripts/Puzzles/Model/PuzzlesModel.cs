using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Dobeil
{
	[System.Serializable]
	public class PuzzleLevelSpirteData
	{
		public int row;
		public int col;
		public string puzzleImageFilePath;
		public void LoadImage(Image result)
		{
			Texture2D texture = DobeilHelper.Instance.LoadTextureFromFile(puzzleImageFilePath, true, puzzleImageFilePath);
			result.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
			result.SetNativeSize();
		}

	}
	[System.Serializable]
	public class PuzzleLevelData
	{
		public int level;
		public string fullImagePath;
		public float puzzleWidth;
		public float puzzleHeight;
		public float normalizedScale;
		public int rowCount;
		public int colCount;
		public List<PuzzleLevelSpirteData> levelData = new List<PuzzleLevelSpirteData>();
		public void LoadFullImage(Image result)
		{
			Texture2D texture = DobeilHelper.Instance.LoadTextureFromFile(fullImagePath, true, fullImagePath);
			result.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
			result.SetNativeSize();
			result.rectTransform.localScale = new Vector3(normalizedScale, normalizedScale, normalizedScale);
		}
	}
	[System.Serializable]
	public class Puzzles
	{
		public List<PuzzleLevelData> puzzleLevel = new List<PuzzleLevelData>();
		
	}
}