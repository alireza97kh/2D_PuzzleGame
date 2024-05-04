using Dobeil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLevelPopUpController : DobeilPageBase
{
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
		AudioManager.Instance.PlaySfx("Win");
	}

	public void OnNextLevelBtnClick(bool nextLevel)
	{
		AudioManager.Instance.PlaySfx("Click");
		int level = GameData.Instance.PlayerProfile.level + (nextLevel ? 1 : 0);
		PuzzleLevelData nextLevelData = GameData.Instance.puzzlesData.puzzlesLevelDatas.puzzleLevel.Find(x => x.level == level);
		if (nextLevelData != null)
		{
			if (nextLevel)
			{
				GameData.Instance.PlayerProfile.level++;
				GameData.Instance.SaveProfile();
			}
			DobeilPageManager.Instance.ShowPageByName("MainGamePlayPage");
		}
	}
}