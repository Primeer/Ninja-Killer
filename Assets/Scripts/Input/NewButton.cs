using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class Vector2Event : UnityEvent<Vector2>
{    
}

public class NewButton : MonoBehaviour
{
	[SerializeField] private Vector2Event onTap;
	[SerializeField] private Vector2Event onDrag;
	[SerializeField] private Vector2Event onRelease;

	private void Start() {
		BoxCollider collider = GetComponent<BoxCollider>();

		if(!collider)
		{
			collider = gameObject.AddComponent<BoxCollider>();
			collider.isTrigger = true;
		}

		RectTransform rectTransform = GetComponent<RectTransform>();
		
		if (rectTransform)
		{
			Vector2 newSize = new Vector2(rectTransform.rect.width, rectTransform.rect.height);
			collider.size = newSize;
		}
	}
	
	private void OnMouseDown() {
		onTap?.Invoke(Input.mousePosition);
	}

	private void OnMouseDrag() {
		onDrag?.Invoke(Input.mousePosition);
	}

	private void OnMouseUp(){
		onRelease?.Invoke(Input.mousePosition);
	}
}
