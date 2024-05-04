using Dobeil;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleSlotController : MonoBehaviour
{
	public bool hasTriggered = false;


	[SerializeField] private Image slotImage;
	[SerializeField] private BoxCollider2D slotCollider;

	private float minDistance;

	private Transform parentTransform;
	private Vector3 startColliderSize = Vector3.one;

	public void Init()
	{
		slotImage.enabled = false;

		parentTransform = transform.parent;
		float _width = slotImage.rectTransform.rect.width;
		float _height = slotImage.rectTransform.rect.height;
		minDistance = (_width < _height) ? _width / 2 : _height / 2;
		startColliderSize = new Vector2(slotImage.rectTransform.rect.width, slotImage.rectTransform.rect.height);
		startColliderSize *= 0.45f;
		slotCollider.size = startColliderSize;



	}

	

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.transform != parentTransform)
		{
			hasTriggered = true;
			slotImage.enabled = hasTriggered;
		}
		
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.transform != parentTransform)
			hasTriggered = false;
		slotImage.enabled = hasTriggered;
	}
}
