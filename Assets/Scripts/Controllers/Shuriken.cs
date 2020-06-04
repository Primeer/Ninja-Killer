using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
	[SerializeField] private float movementSpeed;
	[SerializeField] private float rotationSpeed;
	[SerializeField] private float gravitation;

	private Rigidbody rigidbody;

	private Vector3 velocity;

	public void Init(float angle)
	{
		
		rigidbody = GetComponent<Rigidbody>();
		rigidbody.velocity = Vector3.zero;
		// rigidbody.AddForce(Converter.AngleToDirectionXZ(angle) * movementSpeed * 50, ForceMode.Acceleration);


		StartCoroutine(Movement(angle));
	}

	void Update()
    {
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}

	// private void FixedUpdate() {
	// 	Vector3 targetPos = rigidbody.position;
	// 	targetPos.x = 0;
	// 	rigidbody.AddForce((targetPos - rigidbody.position).normalized * movementSpeed * gravitation / GameManager.Instance.options.trajectoryDistance, ForceMode.Acceleration);
	// }

	private void OnTriggerEnter(Collider other) {
		if (other.transform.root.gameObject.tag == "Victim")
		{
			other.transform.root.gameObject.GetComponent<Enemy>().Death();
			other.GetComponent<Rigidbody>().AddForceAtPosition(velocity.normalized * 1000, rigidbody.position);
		}

		if(other.gameObject.tag == "Wall")
			ShurikenSpawner.Despawn(gameObject);
	}

	private IEnumerator Movement(float angle)
	{
		velocity = Converter.AngleToDirectionXZ(angle);
		Vector3 pos = GameManager.Instance.player.transform.position;
		pos.y += 1.1f;

		float movementTime = GameManager.Instance.options.trajectoryDistance / movementSpeed;
		float acceleration = velocity.x * 2.4f / movementTime;

		float t = 0f;
		while(t < movementTime * 0.95f)
		{
			pos += velocity.normalized * movementSpeed * Time.deltaTime;
			transform.position = pos;
			velocity.x -= acceleration * Time.deltaTime;

			yield return null;
			t += Time.deltaTime;
		}

		velocity.z = -velocity.z;
		acceleration = velocity.x * 2.4f / movementTime;

		while(t < movementTime * 1.5f)
		{
			pos += velocity.normalized * movementSpeed * Time.deltaTime;
			transform.position = pos;
			velocity.x -= acceleration * Time.deltaTime;

			yield return null;
			t += Time.deltaTime;
		}

		ShurikenSpawner.Despawn(gameObject);
	}
}
