using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
	public Obstacle Obstacle
	{
		get 
		{
			gameObject.SetActive(false);
			return transform.parent.GetComponent<Obstacle>(); 
		}
	}

	private void Start() {
		CenterPosition();
	}

	private void CenterPosition()
	{
		Vector3 pos = transform.position;
		pos.x = 0f;
		transform.position = pos;
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, 0.2f);
	}
}
