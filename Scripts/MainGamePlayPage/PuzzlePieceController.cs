using Dobeil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzlePieceController : MonoBehaviour
{
	[SerializeField] private Image pieceImage;
    [SerializeField] private DragAndDrop dragAndDrop;
    [SerializeField] private PuzzleSlotController puzzleSlot;

	private PuzzleLevelSpirteData pieceData;
    private int currentRowState;
    private int currentColState;
	
	public void Init(PuzzleLevelSpirteData _pieceData, int _currentRowState, int _currentColState)
    {
        DobeilEventManager.RegisterGlobalEvent("SwapPieces", SwapPiece);
        DobeilEventManager.RegisterGlobalEvent("OnEndDrag", OnEndDragCallBack);
        RefreshData(_pieceData);
		currentRowState = _currentRowState;
		currentColState = _currentColState;
        puzzleSlot.Init();
	}


	private void OnEndDragCallBack(object obj)
	{
        if (puzzleSlot.hasTriggered)
			DobeilEventManager.SendGlobalEvent("PuzzlePieceClicked", pieceData);
	}

	private void RefreshData(PuzzleLevelSpirteData _pieceData)
	{
		pieceData = _pieceData;
		pieceData.LoadImage(pieceImage);
	}

	private void SwapPiece(object obj)
	{
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

    public bool CheckPuzzlePiece()
    {
        return currentColState == pieceData.col && currentRowState == pieceData.row;
    }

	private void OnDestroy()
	{
        DobeilEventManager.RemoveGlobalEvent("SwapPieces", SwapPiece);
	}
}
