using Dobeil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPageController : DobeilPageBase
{
	[SerializeField] private GameObject addNewLevelObj;
	protected override void HidePage(object data = null)
	{
		
	}

	protected override void SetPageEvents()
	{
		
	}

	protected override void SetPageProperty()
	{
		GameData.Instance.UpdatePuzzleData();
#if UNITY_EDITOR
		addNewLevelObj.SetActive(true);
#else
addNewLevelObj.SetActive(false);
#endif
	}

	protected override void ShowPage(object data = null)
	{
		
	}

	public void OnAddPuzzleBtnClick()
	{
		AudioManager.Instance.PlaySfx("Click");
		DobeilPageManager.Instance.ShowPageByName("PuzzleCreatorPage", true);
	}

	public void OnPlayGameButtonClick()
	{
		AudioManager.Instance.PlaySfx("Click");
		DobeilPageManager.Instance.ShowPageByName("MainGamePlayPage", true);
	}
}
