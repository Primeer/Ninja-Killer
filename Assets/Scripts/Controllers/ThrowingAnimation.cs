using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAnimation : MonoBehaviour
{
	[SerializeField] private float rotationTime;
	[SerializeField] private float idleTime;
	[SerializeField] private float comebackTime;

	private Animator animator;

	private float angle;

	private void Start() {
		animator = GetComponent<Animator>();
	}

	public void Begin(float angle)
	{
		this.angle = angle;
		StartCoroutine(FixRotation(angle));

		animator.SetTrigger("throw");
	}

	private IEnumerator FixRotation(float angle)
	{
		float t = 0f;
		float targetAngle = angle;
		float rotationSpeed = targetAngle / rotationTime;

		while (t < Mathf.Abs(targetAngle))
		{
			float ang = rotationSpeed * Time.deltaTime;
			transform.Rotate(Vector3.up, ang);
			t += Mathf.Abs(ang);
			yield return null;
		}

		t = 0f;

		while (t < idleTime)
		{
			t += Time.deltaTime;
			yield return null;
		}

		StartCoroutine(FixPosition());

		t = 0f;
		rotationSpeed = targetAngle / comebackTime;

		while (t < Mathf.Abs(targetAngle))
		{
			float ang = rotationSpeed * Time.deltaTime;
			transform.Rotate(Vector3.up, -ang);
			t += Mathf.Abs(ang);
			yield return null;
		}

		transform.rotation = Quaternion.identity;
	}

	private IEnumerator FixPosition()
	{
		float t = 0f;
		
		Vector3 targetPos = transform.localPosition;
		targetPos.x = 0f;

		float speed = (targetPos - transform.localPosition).magnitude / comebackTime;
		
		while (t < comebackTime)
		{
			transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, speed * Time.deltaTime);
			t += Time.deltaTime;
			yield return null;
		}
		
		transform.localPosition = targetPos;
	}

	public void OnThrow()
	{
		ShurikenSpawner.Spawn();
	}
}