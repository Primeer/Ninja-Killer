using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
	[SerializeField] private int markerCount => GameManager.Instance.options.markerCount;
	[SerializeField] private GameObject markerPrefab => GameManager.Instance.options.markerPrefab;

	private Trajectory trajectory;
	private List<GameObject> markers = new List<GameObject>();

	private Transform player;
	private GameObject markersContainer;

	private void Start() {
		player = GameManager.Instance.player.transform;
		markersContainer = new GameObject("Markers");
	}

	public void SetAngle(float angle)
	{
		ClearMarkers();

		Vector3 startDirection = Converter.AngleToDirectionXZ(angle);
		
		Vector3 startPos = Vector3.zero;
		startPos.y = player.position.y + 1.1f;
		startPos.z = player.position.z;

		Trajectory.main = new Trajectory(startPos, startDirection, GameManager.Instance.options.trajectoryDistance, GameManager.Instance.options.shurikenSpeed);

		for (int i = 1; i <= markerCount * 2 - 3; i++)
		{
			GameObject marker = Lean.Pool.LeanPool.Spawn(markerPrefab, markersContainer.transform);
			marker.transform.position = Trajectory.main.GetPointByPercent((float)i / (float)markerCount);
			markers.Add(marker);
		}
	}

	public void Hide()
	{
		ClearMarkers();
	}

	private void ClearMarkers()
	{
		foreach(GameObject marker in markers)
		{
			Lean.Pool.LeanPool.Despawn(marker);
		}
		
		markers.Clear();
	}
}
