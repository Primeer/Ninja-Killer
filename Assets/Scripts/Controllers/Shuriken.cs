using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
	[SerializeField] private float lifeTime;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float startPositionAtTrajectory;
	[SerializeField] private float force;

	private Coroutine movement;
	private Coroutine rotation;

	private float life;

	public void Init()
	{
		life = 0f;
		movement = StartCoroutine(Movement());
		rotation = StartCoroutine(Rotation());
	}

	private void OnTriggerEnter(Collider other) {
		TouchableObject touchebleObj = other.GetComponentInParent<TouchableObject>();

		if (touchebleObj != null)
		{
			touchebleObj.Touch(Trajectory.main.GetVelocityByTime(life + startPositionAtTrajectory).normalized * force);
			if(touchebleObj.Solid) 
				Attach(other.transform);
		}
		else
			Attach(other.transform);
	}

	private IEnumerator Movement()
	{
		transform.position = Trajectory.main.GetPointByTime(startPositionAtTrajectory);
		
		while(life < lifeTime)
		{
			yield return null;
			transform.position = Trajectory.main.GetPointByTime(life + startPositionAtTrajectory);
			life += Time.deltaTime;
		}

		ShurikenSpawner.Despawn(gameObject);
	}

	private IEnumerator Rotation()
	{
		while(true)
		{
			transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
			yield return null;
		}
	}

	private void Attach(Transform parent)
	{
		StopCoroutine(movement);
		StopCoroutine(rotation);
		// Destroy(GetComponent<ObiRigidbody>());
		Destroy(GetComponent<Rigidbody>());
		transform.parent = parent;

		ShurikenSpawner.Disable(gameObject);
		Destroy(this);
	}

	void OnDrawGizmosSelected()
    {
		if (Trajectory.main != null)
		{
			Gizmos.color = Color.blue;

			float t = 0f;
			while(t < 2f)
			{
				Vector3 start = Trajectory.main.GetPointByTime(t);
				t += 0.01f;
				Vector3 end = Trajectory.main.GetPointByTime(t);
				Gizmos.DrawLine(start, end);
			}
		}
	}
}
