using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField] private float maxDistance => GameManager.Instance.options.trajectoryDistance;
	[SerializeField] private int markerCount => GameManager.Instance.options.markerCount;
	[SerializeField] private GameObject markerPrefab => GameManager.Instance.options.markerPrefab;

	private List<GameObject> markers = new List<GameObject>();

	public bool Visible
	{
		get; set;
	}

	private Transform player;

	private void Start() {
		player = GameManager.Instance.player.transform;
	}

	public void Set(float angle)
	{
		ClearMarkers();

		Vector3 velocity = Converter.AngleToDirectionXZ(angle);
		Vector3 pos = player.position;
		pos.y += 1.2f;

		float markerDistance = maxDistance / markerCount;

		float acceleration = velocity.x * 2f / (markerCount - 1);

		for (int i = 0; i < markerCount; i++)
		{
			pos += velocity.normalized * markerDistance;

			GameObject marker = Lean.Pool.LeanPool.Spawn(markerPrefab);
			marker.transform.position = pos;
			markers.Add(marker);

			velocity.x -= acceleration;
		}

		for (int i = markerCount - 2; i > markerCount - 4; i--)
		{
			pos = markers[i].transform.position;
			pos.x = -pos.x;

			GameObject marker = Lean.Pool.LeanPool.Spawn(markerPrefab);
			marker.transform.position = pos;
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
