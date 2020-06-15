using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bascketball : TouchableObject
{
	[SerializeField] private float angle;
	[SerializeField] private float fallingSpeed;
	[SerializeField] private float pivotOffset;
	[SerializeField] private GameObject bottle;

	public override void Touch(Vector3 force)
	{
		base.Touch(force);

		StartCoroutine(Fall(angle));
	}

	private IEnumerator Fall(float ang)
	{
		Vector3 pivot = transform.position;
		pivot.y += pivotOffset;

		float angleCounter = 0f;
		
		while(angleCounter < ang)
		{
			transform.RotateAround(pivot, Vector3.forward, fallingSpeed * Time.deltaTime);
			angleCounter += Time.deltaTime;
			yield return null;
		}

		BottleFall();
	}

	private void BottleFall()
	{
		Rigidbody bottleRig = bottle.GetComponent<Rigidbody>();
		bottleRig.isKinematic = false;

		Vector3 forcePosition = bottle.transform.position;
		forcePosition.y += 0.175f;
		bottleRig.AddForceAtPosition(Vector3.left * 20f, forcePosition);
		bottleRig.AddTorque(Vector3.forward * 80f);

		bottle.GetComponent<Collider>().enabled = true;
	}
}
