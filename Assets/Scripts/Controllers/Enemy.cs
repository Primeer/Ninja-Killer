using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, ITouchable
{
	public Obstacle obstacle;

	public void Touch()
	{
		Death();
	}

	public void Death()
	{
		obstacle.Delete(gameObject);
	}
}
