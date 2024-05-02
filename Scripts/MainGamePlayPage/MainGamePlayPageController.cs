using Dobeil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainGamePlayPageController : DobeilPageBase
{

	[SerializeField] private Image completedPuzzleImage;
	[SerializeField] private GameObject puzllePiecesObject;
	[SerializeField] private GridLayoutGroup puzllePiecesGrid;

	[SerializeField] private GameObject showFullImageButton;

	[SerializeField] private float showFullImageStartDellay = 3;
	[SerializeField] private float showFullImageInGameDellay = 1;

	private int level;
	private PuzzleLevelData currentLevelData;

	private PuzzlePieceController puzzlePiecePrefab;
	private PuzzleLevelSpirteData selectedPuzzlePiece = null;

	private List<PuzzlePieceController> currentLevelPieces = new List<PuzzlePieceController>();
	private bool showingFullImage = false;
	protected override void HidePage(object data = null)
	{
		
	}

	protected override void SetPageEvents()
	{
		DobeilEventManager.RegisterGlobalEvent("PuzzlePieceClicked", OnPuzzlePieceClicked);
	}

	private void OnPuzzlePieceClicked(object obj)
	{
		if (obj is PuzzleLevelSpirteData)
		{
			if (selectedPuzzlePiece == null)
				selectedPuzzlePiece = obj as PuzzleLevelSpirteData;

			else
			{
				List<PuzzleLevelSpirteData> swapList = new List<PuzzleLevelSpirteData>
				{
					selectedPuzzlePiece,
					obj as PuzzleLevelSpirteData
				};
				selectedPuzzlePiece = null;
				DobeilEventManager.SendGlobalEvent("SwapPieces", swapList);
				if (CheckPuzzleIsComplete())
					DobeilPageManager.Instance.ShowPageByName("WinPuzzlePopUp");

			}
		}
	}

	protected override void SetPageProperty()
	{
		puzzlePiecePrefab = VisualDataMenu.Instance.MainGamePlayPageData.puzzlePiecePrefab;
	}

	protected override void ShowPage(object data = null)
	{
		level = GameData.Instance.PlayerProfile.level;
		currentLevelData = GameData.Instance.puzzlesData.puzzlesLevelDatas.puzzleLevel.Find(x => x.level == level);
		if (currentLevelData == null)
			Back();
		RefreshUi();
		currentLevelData.LoadFullImage(completedPuzzleImage);
		puzllePiecesObject.SetActive(false);
		showFullImageButton.SetActive(false);

		puzllePiecesGrid.constraintCount = currentLevelData.colCount;
		puzllePiecesGrid.cellSize = new Vector2(currentLevelData.puzzleWidth / currentLevelData.colCount, currentLevelData.puzzleHeight / currentLevelData.rowCount);


		currentLevelData.levelData = ShuffleList(currentLevelData.levelData);
		int index = 0;
		for (int i = 0; i < currentLevelData.rowCount; i++)
		{
			for (int j = 0; j < currentLevelData.colCount; j++)
			{
				PuzzlePieceController newPuzzlePiece = Instantiate(puzzlePiecePrefab, puzllePiecesGrid.transform);
				newPuzzlePiece.Init(currentLevelData.levelData[index], i, j);
				currentLevelPieces.Add(newPuzzlePiece);
				index++;
			}
		}
		SetPuzzlePiecesGrid();
	}

	private void RefreshUi()
	{
		foreach (var item in currentLevelPieces)
		{
			Destroy(item.gameObject);
		}
		currentLevelPieces.Clear();
	}

	private async void SetPuzzlePiecesGrid()
	{
		await Task.Delay(System.TimeSpan.FromSeconds(showFullImageStartDellay));
		puzllePiecesObject.SetActive(true);
		showFullImageButton.SetActive(true);
	}

	private List<PuzzleLevelSpirteData> ShuffleList(List<PuzzleLevelSpirteData> puzzleLevelSpirteDatas)
	{
		for (int i = puzzleLevelSpirteDatas.Count - 1; i > 0; i--)
		{
			int j = UnityEngine.Random.Range(0, i + 1);
			PuzzleLevelSpirteData temp = puzzleLevelSpirteDatas[i];
			puzzleLevelSpirteDatas[i] = puzzleLevelSpirteDatas[j];
			puzzleLevelSpirteDatas[j] = temp;
		}
		return puzzleLevelSpirteDatas;

	}

	private bool CheckPuzzleIsComplete()
	{
		foreach (var piece in currentLevelPieces)
		{
			if (!piece.CheckPuzzlePiece())
				return false;
		}
		return true;
	}


	public void OnShowFullImageBtnClick()
	{
		if (!showingFullImage)
		{
			showingFullImage = true;
			puzllePiecesObject.SetActive(false);
			ShowFullImage();
		}
	}

	private async void ShowFullImage()
	{
		await Task.Delay(TimeSpan.FromSeconds(showFullImageInGameDellay));
		puzllePiecesObject.SetActive(true);
		showingFullImage = false;
	}
}
