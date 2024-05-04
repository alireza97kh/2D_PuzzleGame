using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Networking;
using System.IO;
public class DobeilFileBrowser : MonoSingleton<DobeilFileBrowser>
{
	public string saveDirectory = "";

	protected override void Awake()
	{
		base.Awake();
		saveDirectory = "Assets/Texture/_PuzzlesFullImages";
	}

	public void GetImageFromFileBrowser(Action<string> OnDoneLoadImage)
	{
#if UNITY_EDITOR

		// Open a file dialog to let the user select an image file
		string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select Image", "", "png,jpg");

		if (!string.IsNullOrEmpty(imagePath))
		{
			// Copy the selected image file to the project's Assets folder
			string fileName = Path.GetFileName(imagePath);
			string uniqueFileName = UnityEditor.AssetDatabase.GenerateUniqueAssetPath(Path.Combine(saveDirectory, fileName));

			UnityEditor.FileUtil.CopyFileOrDirectory(imagePath, uniqueFileName);
			UnityEditor.AssetDatabase.Refresh();

			UnityEditor.AssetDatabase.Refresh();
			OnDoneLoadImage(uniqueFileName);
		}
#endif
	}


}
