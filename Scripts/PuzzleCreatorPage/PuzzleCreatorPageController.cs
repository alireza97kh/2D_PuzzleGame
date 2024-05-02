using Dobeil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Application = UnityEngine.Application;

public class PuzzleCreatorPageController : DobeilPageBase
{
	[SerializeField] private Image selectedImage;
	[SerializeField] private TMP_InputField levlInput;
	[SerializeField] private TMP_InputField rowCountInput;
	[SerializeField] private TMP_InputField columnCountInput;

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
		if (data != null && data is Sprite)
		{
			selectedImage.sprite = (Sprite)data;
		}
		else
		{
			DobeilPageManager.Instance.BackToLastPage();
		}
	}
	public void OnCreatePuzzleButtonClick()
	{
        if (String.IsNullOrEmpty(rowCountInput.text) || String.IsNullOrEmpty(columnCountInput.text))
			return;
        int _row = int.Parse(rowCountInput.text);
		int _col = int.Parse(columnCountInput.text);

		Puzzles puzzles = SaveManager<Puzzles>.LoadData("PuzzleData");
		if (puzzles == null)
		{
			puzzles = new Puzzles();
		}
		puzzles.puzzleLevel.Add(new PuzzleLevelData()
		{
			level = int.Parse(levlInput.text),
			levelData = SpliteImage(_row, _col, GetTextureFromSprite(selectedImage.sprite))
		});
		SaveManager<Puzzles>.SaveData("PuzzleData", puzzles);
		GameData.Instance.UpdatePuzzleData();

		DobeilPageManager.Instance.ShowPageByName("MainMenu", flushPageStack: true);
	}
	private List<PuzzleLevelSpirteData> SpliteImage(int rows, int cols, Texture2D puzzleImage)
	{
		int pieceWidth = puzzleImage.width / cols;
		int pieceHeight = puzzleImage.height / rows;
		List<PuzzleLevelSpirteData> result = new List<PuzzleLevelSpirteData>();
		string createImagePath = Application.dataPath + "/Texture/_Levels/" + levlInput.text;
		if (Directory.Exists(createImagePath))
			FileUtil.DeleteFileOrDirectory(createImagePath);

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

		return result;


	}
	Texture2D GetTextureFromSprite(Sprite sprite)
	{
		if (sprite.texture != null)
		{
			// If the sprite has a texture, return it
			return sprite.texture;
		}
		else
		{
			Texture2D texture = new Texture2D((int)sprite.textureRect.width, (int)sprite.textureRect.height);
			texture.SetPixels(sprite.texture.GetPixels((int)sprite.textureRect.x,
													  (int)sprite.textureRect.y,
													  (int)sprite.textureRect.width,
													  (int)sprite.textureRect.height));
			texture.Apply();
			return texture;
		}
	}

}
