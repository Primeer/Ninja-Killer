using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : TouchableObject
{
    [HideInInspector] public Obstacle obstacle;

	private bool isDead;

	private void OnCollisionEnter(Collision other) {
		Death();
	}

	public override void Touch(Vector3 force)
	{
		base.Touch(force);

		Death();
	}

	public void Death()
	{
		if(isDead) return;

		isDead = true;
		GetComponent<Animator>().enabled = false;
		obstacle.Remove(gameObject);
	}
}
