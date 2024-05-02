using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileBrowserPageController : DobeilPageBase
{
	[SerializeField] private Image resultImage;
	[SerializeField] private Sprite defaultSprite;
	private string loadedImageFilePath = string.Empty;
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

	public void OnBrowserFileButtonClick()
	{
		DobeilFileBrowser.Instance.GetImageFromFileBrowser(OnDonLoadImage);
	}

	private void OnDonLoadImage(string filePath)
	{
		// Load the saved image file as a texture
		Texture2D texture = DobeilHelper.Instance.LoadTextureFromFile(filePath, true, filePath);

		if (texture != null)
		{
			resultImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
		}
		else
		{
			Debug.LogError("Failed to load texture from file: " + filePath);
		}
	}

	public void SaveImageButtonClick()
	{
		DobeilPageManager.Instance.ShowPageByName("PuzzleCreatorPage", true, resultImage.sprite);
	}
}
