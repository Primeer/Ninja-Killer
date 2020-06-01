using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	private List<GameObject> victims = new List<GameObject>();
	public Task task;

	private void Start() {
		foreach(Transform obj in transform)
		{
			if(obj.gameObject.tag == "Victim")
				victims.Add(obj.gameObject);
		}
	}
	
	public void Delete(GameObject victim)
	{
		Destroy(victim);
		victims.Remove(victim);

		if(victims.Count == 0)
			task.completed = true;
	}
}
