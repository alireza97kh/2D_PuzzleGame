using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using AnotherFileBrowser.Windows;
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
		// Open a file dialog to let the user select an image file
		string imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select Image", "", "png,jpg");

		if (!string.IsNullOrEmpty(imagePath))
		{
			// Copy the selected image file to the project's Assets folder
			string fileName = Path.GetFileName(imagePath);
			string uniqueFileName = AssetDatabase.GenerateUniqueAssetPath(Path.Combine(saveDirectory, fileName));

			FileUtil.CopyFileOrDirectory(imagePath, uniqueFileName);
			AssetDatabase.Refresh();

			UnityEditor.AssetDatabase.Refresh();
			OnDoneLoadImage(uniqueFileName);
		}
	}

	
}
