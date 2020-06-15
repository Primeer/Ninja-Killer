using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : TouchableObject
{
	private RopeCutter cutter;

	private void Start() {
		cutter = GetComponentInChildren<RopeCutter>();
	}

	public override void Touch(Vector3 force)
	{
		base.Touch(force);

		cutter?.Cut();
		GetComponent<Collider>().enabled = false;
	}
}
