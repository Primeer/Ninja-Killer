using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
	[SerializeField] private Vector3 offset;
	[SerializeField] private Vector3 size;

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawCube(transform.position + offset, size);
	}
}
