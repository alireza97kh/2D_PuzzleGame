using Dobeil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PuzzlesData", menuName = "Puzzles/PuzzlesData")]
public class PuzzlesData : ScriptableObject
{
    public Puzzles puzzlesLevelDatas;

    public void FillPuzzleLevelData()
    {
        puzzlesLevelDatas = SaveManager<Puzzles>.LoadData("PuzzleData");
        if (puzzlesLevelDatas == null)
        {
            puzzlesLevelDatas = new Puzzles();
			SaveManager<Puzzles>.SaveData("PuzzleData", puzzlesLevelDatas);
		}

	}
}
