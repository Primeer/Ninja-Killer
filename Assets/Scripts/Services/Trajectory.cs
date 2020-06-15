using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory
{
	public static Trajectory main;

	public Vector3 startPoint;
	public Vector3 endPoint;
	public Vector3 startVelocity;
	public float movementSpeed;
	public float movementTime;
	public Vector3 acceleration;
	public float distance;

	private float timeCounter;

	public Trajectory(Vector3 point, Vector3 dir, float dist, float speed)
	{
		startPoint = point;
		startVelocity = dir * speed;
		distance = dist;
		movementSpeed = speed;
		movementTime = dist / speed;
		acceleration = new Vector3(-(2 * startVelocity.x) / movementTime, 0, 0);
		endPoint = GetPointByTime(movementTime);
	}

	public Vector3 GetPointByTime(float t)
	{
		timeCounter = t;
		Vector3 point = startPoint + startVelocity * t + acceleration * t * t / 2f;
		if(t > movementTime) return GetReversePoint(t);
		return point;
	}

	public Vector3 GetPointByDeltaTime(float t)
	{
		timeCounter += t;
		return GetPointByTime(timeCounter);
	}

	public Vector3 GetPointByPercent(float percent)
	{
		float time = movementTime * percent;
		return GetPointByTime(time);
	}

	private Vector3 GetReversePoint(float t)
	{
		t -= movementTime;
		Vector3 point = endPoint - startVelocity * t - acceleration * t * t / 2f;
		return point;
	}

	public Vector3 GetVelocityByTime(float t)
	{
		Vector3 velocity = startVelocity + acceleration * t;
		if(t > movementTime) return GetReverseVelocity(t);
		return velocity;
	}

	private Vector3 GetReverseVelocity(float t)
	{
		t -= movementTime;
		Vector3 velocity = -startVelocity - acceleration * t;
		return velocity;
	}

	public void ResetTime() => timeCounter = 0f;
}
