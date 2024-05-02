using Dobeil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceController : MonoBehaviour
{

    [SerializeField] private Image pieceImage;
	[SerializeField] private GameObject highLighteObject;

    private PuzzleLevelSpirteData pieceData;
    private int currentRowState;
    private int currentColState;
	public void Init(PuzzleLevelSpirteData _pieceData, int _currentRowState, int _currentColState)
    {
        DobeilEventManager.RegisterGlobalEvent("SwapPieces", SwapPiece);
        RefreshData(_pieceData);
		currentRowState = _currentRowState;
		currentColState = _currentColState;
	}

	private void RefreshData(PuzzleLevelSpirteData _pieceData)
	{
		pieceData = _pieceData;
		pieceData.LoadImage(pieceImage);
	}

	private void SwapPiece(object obj)
	{
		highLighteObject.SetActive(false);
		if (obj == null) return;
        if (obj is List<PuzzleLevelSpirteData>)
        {
            List<PuzzleLevelSpirteData> swapList = obj as List<PuzzleLevelSpirteData>;
            int index = swapList.FindIndex(x => x == pieceData);
			if (index != -1)
            {
                switch (index)
                {
                    case 0:
                        pieceData = swapList[1];
                        break;
                    case 1:
                        pieceData = swapList[0];
                        break;
                    default:
                        break;
                }
                RefreshData(pieceData);
            }
        }
	}

	public void OnPuzzlePieceClick()
    {
        highLighteObject.SetActive(true);
        DobeilEventManager.SendGlobalEvent("PuzzlePieceClicked", pieceData);

	}

    public bool CheckPuzzlePiece()
    {
        return currentColState == pieceData.col && currentRowState == pieceData.row;
    }

	private void OnDestroy()
	{
        DobeilEventManager.RemoveGlobalEvent("SwapPieces", SwapPiece);
	}
}
