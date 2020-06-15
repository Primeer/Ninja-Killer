using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public int attemptCount;
	public float delay = 1f;
	private List<GameObject> enemies = new List<GameObject>();
	public Task task;

	private void Start() {
		foreach (Transform obj in transform)
		{
			if (obj.gameObject.tag == "Enemy")
				enemies.Add(obj.gameObject);

			Character character = obj.GetComponent<Character>();
			if(character)
				character.obstacle = this;
		}
	}
	
	public void Remove(GameObject character)
	{
		if(character.GetComponent<Hostage>() != null)
		{
			task.Fail();
			return;
		}

		enemies.Remove(character);

		if(enemies.Count == 0)
			task.Complete();
	}
}
