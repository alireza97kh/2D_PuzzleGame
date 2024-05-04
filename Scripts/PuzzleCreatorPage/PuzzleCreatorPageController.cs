using Dobeil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

public class PuzzleCreatorPageController : DobeilPageBase
{
	[SerializeField] private RectTransform puzzleImageParent;
	[SerializeField] private Image resultImage;
	[SerializeField] private Sprite defaultSprite;
	[SerializeField] private TMP_InputField levelInput;
	[SerializeField] private TMP_InputField rowCountInput;
	[SerializeField] private TMP_InputField columnCountInput;

	private PuzzleLevelData newPuzzleLevel;

	protected override void HidePage(object data = null)
	{
		
	}

	protected override void SetPageEvents()
	{
		
	}

	protected override void SetPageProperty()
	{
		
	}
	protected override void ShowPage(object data = null)
	{
#if !UNITY_EDITOR
		Back();
#else
		resultImage.sprite = defaultSprite;
		SetLevelText();
#endif
	}
	#region LoadNewImage
	public void OnBrowserFileButtonClick()
	{
		AudioManager.Instance.PlaySfx("Click");
		DobeilFileBrowser.Instance.GetImageFromFileBrowser(OnDonLoadImage);
	}
	private void OnDonLoadImage(string filePath)
	{
		Texture2D texture = DobeilHelper.Instance.LoadTextureFromFile(filePath, true, filePath);

		if (texture != null)
		{
			resultImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
			resultImage.SetNativeSize();
			float _scale = NormalizeLoadedPuzzleRect(texture);
			resultImage.rectTransform.localScale = new Vector3(_scale, _scale, _scale);
			newPuzzleLevel = new PuzzleLevelData()
			{
				fullImagePath = filePath,
				levelData = new List<PuzzleLevelSpirteData>(),
				normalizedScale = _scale,
				puzzleWidth = texture.width,
				puzzleHeight = texture.height
			};
		}
		else
		{
			DobeilLogger.LogError("Failed to load texture from file: " + filePath);
		}
	}
	private float NormalizeLoadedPuzzleRect(Texture2D texure)
	{
		return puzzleImageParent.rect.width / texure.width;
	}
	#endregion
	#region Split Loaded Image
	public void OnCreatePuzzleButtonClick()
	{
        if (String.IsNullOrEmpty(rowCountInput.text) || String.IsNullOrEmpty(columnCountInput.text) || String.IsNullOrEmpty(levelInput.text))
			return;
		AudioManager.Instance.PlaySfx("Click");
		newPuzzleLevel.rowCount = int.Parse(rowCountInput.text);
		newPuzzleLevel.colCount = int.Parse(columnCountInput.text);
		newPuzzleLevel.level = int.Parse(levelInput.text);
		newPuzzleLevel.levelData = SpliteImage(newPuzzleLevel.rowCount, newPuzzleLevel.colCount, DobeilHelper.Instance.GetTextureFromSprite(resultImage.sprite));




		Puzzles puzzles = SaveManager<Puzzles>.LoadData("PuzzleData");
		if (puzzles == null)
		{
			puzzles = new Puzzles();
		}
		int _index = puzzles.puzzleLevel.FindIndex(x => x.level == newPuzzleLevel.level);
		if (_index != -1)
			puzzles.puzzleLevel.RemoveAt(_index);


		puzzles.puzzleLevel.Add(newPuzzleLevel);
		SaveManager<Puzzles>.SaveData("PuzzleData", puzzles);
		GameData.Instance.UpdatePuzzleData();
		Back();
	}
	private List<PuzzleLevelSpirteData> SpliteImage(int rows, int cols, Texture2D puzzleImage)
	{
		List<PuzzleLevelSpirteData> result = new List<PuzzleLevelSpirteData>();
#if UNITY_EDITOR

		int pieceWidth = puzzleImage.width / cols;
		int pieceHeight = puzzleImage.height / rows;
		
		string createImagePath = Application.dataPath + "/Texture/_Levels/" + levelInput.text;
		if (Directory.Exists(createImagePath))
			UnityEditor.FileUtil.DeleteFileOrDirectory(createImagePath);

		Directory.CreateDirectory(createImagePath);

		for (int row = 0; row < rows; row++)
		{
			for (int col = 0; col < cols; col++)
			{
				int x = col * pieceWidth;
				int y = (rows - 1 - row) * pieceHeight; 

				Texture2D pieceTexture = new Texture2D(pieceWidth, pieceHeight);
				pieceTexture.SetPixels(puzzleImage.GetPixels(x, y, pieceWidth, pieceHeight));
				pieceTexture.Apply();

				//Sprite sprite = Sprite.Create(pieceTexture, new Rect(0, 0, pieceWidth, pieceHeight), Vector2.zero);
				byte[] pngBytes = pieceTexture.EncodeToPNG();
				string filename = Path.Combine(createImagePath, "Piece_" + row + "_" + col + ".png");
				File.WriteAllBytes(filename, pngBytes);

				result.Add(new PuzzleLevelSpirteData()
				{
					row = row,
					col = col,
					puzzleImageFilePath = filename
				});
			}
		}

#endif
		return result;
	}
	#endregion
	private void SetLevelText()
	{
		int levelCount = GameData.Instance.puzzlesData.puzzlesLevelDatas.puzzleLevel.Count;
		levelInput.text = (levelCount + 1).ToString();
	}

}
