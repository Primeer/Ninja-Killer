using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
	[SerializeField] private float lifeTime;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float gravitation;

	private float life;

	public void Init()
	{
		life = 0f;
		StartCoroutine(Movement());
	}

	void Update()
    {
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other) {
		ITouchable touchebleObj = other.transform.root.GetComponent<ITouchable>();
		
		if(touchebleObj != null)
			touchebleObj.Touch();
		else
			ShurikenSpawner.Despawn(gameObject);
	}

	private IEnumerator Movement()
	{
		transform.position = Trajectory.main.GetPointByPercent(0.22f);
		
		while(life < lifeTime)
		{
			yield return null;
			transform.position = Trajectory.main.GetPointByDeltaTime(Time.deltaTime);
			life += Time.deltaTime;
		}

		ShurikenSpawner.Despawn(gameObject);
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
