using Dobeil;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "PuzzlesData", menuName = "Puzzles/PuzzlesData")]
public class PuzzlesData : ScriptableObject
{
    public Puzzles puzzlesLevelDatas;

    public void FillPuzzleLevelData()
    {
        if (puzzlesLevelDatas == null)
        {
			puzzlesLevelDatas = SaveManager<Puzzles>.LoadData("PuzzleData");
			if (puzzlesLevelDatas == null)
			{
				puzzlesLevelDatas = new Puzzles();
				SaveManager<Puzzles>.SaveData("PuzzleData", puzzlesLevelDatas);
			}
		}

		puzzlesLevelDatas.puzzleLevel = puzzlesLevelDatas.puzzleLevel.OrderBy(x => x.level).ToList();
	}
}
