using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "VisualDataMenu", menuName = "Puzzles/VisualDataMenu")]
public class VisualDataMenu : ScriptableObject
{
	private static VisualDataMenu instance;
	public static VisualDataMenu Instance
	{
		get
		{
			if (instance == null)
				instance = Resources.Load<VisualDataMenu>("VisualDataMenu/VisualDataMenu");
			return instance;
		}
	}

	public MainGamePlayPageData MainGamePlayPageData;
}
