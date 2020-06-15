using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftButton : TouchableObject
{
	[SerializeField] private float openPosition;
	[SerializeField] private float speed;
	[SerializeField] private GameObject door_l;
	[SerializeField] private GameObject door_r;

	public override void Touch(Vector3 force)
	{
		base.Touch(force);

		StartCoroutine(OpenDoors());
	}

	private IEnumerator OpenDoors()
	{
		float pos = 0f;
		while(pos < openPosition)
		{
			door_l.transform.Translate(Vector3.left * speed * Time.deltaTime);
			door_r.transform.Translate(Vector3.right * speed * Time.deltaTime);
			pos += speed * Time.deltaTime;
			yield return null;
		}
	}
}
