using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
	public int attemptCount;
	private List<GameObject> victims = new List<GameObject>();
	public Task task;

	private void Start() {
		foreach (Transform obj in transform)
		{
			if (obj.gameObject.tag == "Victim")
				victims.Add(obj.gameObject);
		}
		
		foreach(GameObject enemy in victims)
		{
			enemy.transform.parent = null;
			enemy.GetComponent<Enemy>().obstacle = this;
			
		}
	}
	
	public void Delete(GameObject victim)
	{
		// Destroy(victim);
		
	    // victim.GetComponent<CapsuleCollider>().enabled = false;
	    victim.GetComponent<Animator>().enabled = false;
		// Vector3 pos = victim.transform.position;
		// pos.y += 2f;
		// victim.GetComponent<Animator>().SetTrigger("death");

		victims.Remove(victim);

		if(victims.Count == 0)
			task.completed = true;
	}
}
