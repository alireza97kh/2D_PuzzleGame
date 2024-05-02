using Dobeil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPageController : DobeilPageBase
{
	protected override void HidePage(object data = null)
	{
		
	}

	protected override void SetPageEvents()
	{
		
	}

	protected override void SetPageProperty()
	{
		GameData.Instance.UpdatePuzzleData();
		//SaveManager<PlayerProfile>.SaveData("PlayerProfile", new PlayerProfile()
		//{
		//	lastPuzzleState = new PuzzleLevelData(),
		//	level = 1
		//});
	}

	protected override void ShowPage(object data = null)
	{
		
	}

	public void OnAddPuzzleBtnClick()
	{
		DobeilPageManager.Instance.ShowPageByName("PuzzleCreatorPage", true);
	}

	public void OnPlayGameButtonClick()
	{
		DobeilPageManager.Instance.ShowPageByName("MainGamePlayPage", true);
	}
}
