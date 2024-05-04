using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private Vector3 screenPosition;
	private Vector3 offset;

	private Vector3 startPosition;
	private int startSibblingIndex;
	private int parentChildCount;

	public void OnBeginDrag(PointerEventData eventData)
	{
		parentChildCount = transform.parent.childCount - 1;
		startPosition = transform.position;

		screenPosition = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z));
		transform.SetSiblingIndex(parentChildCount);
	}

	public void OnDrag(PointerEventData eventData)
	{
		DragginManager(Input.mousePosition);
	}

	private void DragginManager(Vector3 mousePosition)
	{
		Vector3 currentScreenSpace = new Vector3(mousePosition.x, mousePosition.y, screenPosition.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
		transform.position = currentPosition;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		transform.position = startPosition;
		transform.SetSiblingIndex(startSibblingIndex);
		DobeilEventManager.SendGlobalEvent("OnEndDrag", null);
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.LogError("Bingooo  " + other.name);
	}
}
