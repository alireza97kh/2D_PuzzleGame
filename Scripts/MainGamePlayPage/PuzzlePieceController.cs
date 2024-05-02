using Dobeil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzlePieceController : MonoBehaviour
{

    [SerializeField] private Image pieceImage;

    private PuzzleLevelSpirteData pieceData;
    private int currentRowState;
    private int currentColState;
	public void Init(PuzzleLevelSpirteData _pieceData, int _currentRowState, int _currentColState)
    {
        pieceData = _pieceData;
        currentRowState = _currentRowState;
        currentColState = _currentColState;
        pieceData.LoadImage(pieceImage);
	}
}
