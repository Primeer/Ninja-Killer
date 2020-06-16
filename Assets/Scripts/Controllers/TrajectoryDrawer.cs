using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryDrawer : MonoBehaviour
{
	[Range(0, 50)]
	[SerializeField] private float angle;
	[SerializeField] private Transform playerPosition;
	[SerializeField] private Options options;
	private Trajectory trajectory;

	private void OnDrawGizmosSelected() {
		Gizmos.color = Color.blue;

		Vector3 startDirection = Converter.AngleToDirectionXZ(angle);
		Vector3 startPosition = playerPosition.position + new Vector3(0f, 0.36f, 0.462f);

		trajectory = new Trajectory(startPosition, startDirection, options.trajectoryDistance, options.shurikenSpeed);

		float time = options.trajectoryDistance / options.shurikenSpeed * 2f;
		float t = 0f;
		while(t < time)
		{
			Vector3 start = trajectory.GetPointByTime(t);
			t += 0.01f;
			Vector3 end = trajectory.GetPointByTime(t);
			Gizmos.DrawLine(start, end);
		}
	}
}
