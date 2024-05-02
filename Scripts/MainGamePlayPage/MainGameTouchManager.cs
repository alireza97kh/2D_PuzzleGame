using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainGameTouchManager : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler
{
	public RectTransform rectTransform;
	//private Canvas canvas;
	public CanvasGroup canvasGroup;
	private bool isDragging = false;
	public float canvasScaleFactor = 1;

	// Optional: Variables for snapping to grid
	public bool snapToGrid = false;
	public Vector2 gridSize = new Vector2(100f, 100f);

	private void Awake()
	{
		//rectTransform = GetComponent<RectTransform>();
		////canvas = GetComponentInParent<Canvas>();
		//canvasGroup = GetComponent<CanvasGroup>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		// Start dragging
		isDragging = true;
		canvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (isDragging)
		{
			// Move puzzle piece with the mouse
			rectTransform.anchoredPosition += eventData.delta / canvasScaleFactor;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// Stop dragging
		isDragging = false;
		canvasGroup.blocksRaycasts = true;

		// Optional: Snap to grid
		if (snapToGrid)
		{
			SnapToGrid();
		}
	}

	private void SnapToGrid()
	{
		Vector2 snappedPosition = new Vector2(
			Mathf.Round(rectTransform.anchoredPosition.x / gridSize.x) * gridSize.x,
			Mathf.Round(rectTransform.anchoredPosition.y / gridSize.y) * gridSize.y
		);

		rectTransform.anchoredPosition = snappedPosition;
	}
}
