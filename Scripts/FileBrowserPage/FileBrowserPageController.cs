using Dobeil;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FileBrowserPageController : DobeilPageBase
{
	[SerializeField] private RectTransform puzzleImageParent;
	[SerializeField] private Image resultImage;
	[SerializeField] private Sprite defaultSprite;
	private Puzzles newPuzzle;
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
		resultImage.sprite = defaultSprite;
	}

	

	

	

	public void SaveImageButtonClick()
	{
		if (newPuzzle != null)
		{
			DobeilPageManager.Instance.ShowPageByName("PuzzleCreatorPage", true, newPuzzle);
		}
	}
}
