using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameData : MonoSingleton<GameData>
{
    public PuzzlesData puzzlesData;
	

    public void UpdatePuzzleData()
    {
		puzzlesData.FillPuzzleLevelData();
	}
}
