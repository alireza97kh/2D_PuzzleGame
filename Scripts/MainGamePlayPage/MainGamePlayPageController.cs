using Dobeil;
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

	[SerializeField] private float showFullImageDellay = 3;
	private int level;
	private PuzzleLevelData currentLevelData;

	private PuzzlePieceController puzzlePiecePrefab;
	protected override void HidePage(object data = null)
	{
		
	}

	protected override void SetPageEvents()
	{
		
	}

	protected override void SetPageProperty()
	{
		puzzlePiecePrefab = VisualDataMenu.Instance.MainGamePlayPageData.puzzlePiecePrefab;
	}

	protected override void ShowPage(object data = null)
	{
		level = GameData.Instance.PlayerProfile.level;
		currentLevelData = GameData.Instance.puzzlesData.puzzlesLevelDatas.puzzleLevel.Find(x => x.level == level);
		if (currentLevelData == null )
			Back();
		currentLevelData.LoadFullImage(completedPuzzleImage);
		puzllePiecesObject.SetActive(false);

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
				index++;
			}
		}
		SetPuzzlePiecesGrid();
	}


	private async void SetPuzzlePiecesGrid()
	{
		await Task.Delay(System.TimeSpan.FromSeconds(showFullImageDellay));
		puzllePiecesObject.SetActive(true);
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
}
