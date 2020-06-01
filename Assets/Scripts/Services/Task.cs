﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
	public Obstacle obstacle;
	public bool active;
	public bool completed;

	public Task(Obstacle obs)
	{
		obstacle = obs;
		obstacle.task = this;
	}
}
