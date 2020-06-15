using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
	public Obstacle obstacle;
	public int shurikenCount => obstacle.attemptCount;
	public float delayAfterTask => obstacle.delay;
	public bool active;
	public bool completed;
	public delegate void TaskMethod();
	public event TaskMethod onComplete;
	public event TaskMethod onFail;

	public Task(Obstacle obs)
	{
		obstacle = obs;
		obstacle.task = this;
	}

	public void Complete()
	{
		if(onComplete != null)
			onComplete();
	}

	public void Fail()
	{
		if(onFail != null)
			onFail();
	}
}
