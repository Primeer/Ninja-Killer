using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Obstacle obstacle;

	public void Death()
	{
		obstacle.Delete(gameObject);
	}
}
