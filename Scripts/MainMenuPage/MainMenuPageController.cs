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
	}

	protected override void ShowPage(object data = null)
	{
		
	}

	public void OnAddPuzzleBtnClick()
	{
		DobeilPageManager.Instance.ShowPageByName("FileBrowserPage", true);
	}
}