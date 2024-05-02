using UnityEngine;
using System;

public static class SaveManager<T>
{
	// Save the data to PlayerPrefs
	public static void SaveData(string key, T data)
	{
		try
		{
			string jsonData = JsonUtility.ToJson(data);
			PlayerPrefs.SetString(key, jsonData);
			PlayerPrefs.Save();
			Debug.Log($"Data saved to {key}: {jsonData}");
		}
		catch (Exception e)
		{
			Debug.LogError($"Error saving data to {key}: {e.Message}");
		}
	}

	// Load the data from PlayerPrefs
	public static T LoadData(string key)
	{
		try
		{
			string jsonData = PlayerPrefs.GetString(key, string.Empty);
			if (!string.IsNullOrEmpty(jsonData))
			{
				T loadedData = JsonUtility.FromJson<T>(jsonData);
				Debug.Log($"Data loaded from {key}: {jsonData}");
				return loadedData;
			}
			else
			{
				Debug.LogWarning($"No data found for {key}");
				return default(T);
			}
		}
		catch (Exception e)
		{
			Debug.LogError($"Error loading data from {key}: {e.Message}");
			return default(T);
		}
	}
}
