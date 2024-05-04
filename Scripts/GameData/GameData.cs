using Dobeil;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameData : MonoSingleton<GameData>
{
	private PlayerProfile playerProfile;


    public PuzzlesData puzzlesData;
	public PlayerProfile PlayerProfile
	{
		get
		{
			if (playerProfile == null)
			{
				playerProfile = LoadPlayerProfile();
			}
			return playerProfile;
		}
	}


	public void UpdatePuzzleData()
    {
		puzzlesData.FillPuzzleLevelData();
	}

	private PlayerProfile LoadPlayerProfile()
	{
		playerProfile = SaveManager<PlayerProfile>.LoadData("PlayerProfile");
		if (playerProfile == null )
		{
			playerProfile = new PlayerProfile()
			{
				level = 1,
				lastPuzzleState = null
			};
			SaveManager<PlayerProfile>.SaveData("PlayerProfile", playerProfile);
		}
		return playerProfile;
	}

	public void SaveProfile()
	{
		SaveManager<PlayerProfile>.SaveData("PlayerProfile", playerProfile);
	}
}
