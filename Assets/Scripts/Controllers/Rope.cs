using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour, ITouchable
{
	private RopeCutter cutter;

	private void Start() {
		cutter = GetComponentInChildren<RopeCutter>();
	}

	public void Touch()
	{
		cutter?.Cut();
		GetComponent<Collider>().enabled = false;
	}
}
