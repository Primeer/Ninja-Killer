using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCounter : Singleton<ThrowCounter>
{
	[SerializeField] private GameObject iconPrefab;

	public bool Empty
	{
		get { return count == maxCount; }
	}

	private static int maxCount;
	private static int count;
	private List<GameObject> icons = new List<GameObject>();
	
	public void Count()
	{
		count++;

		RemoveIcon();
	}

	public void ResetCount(int max)
	{
		ClearIcons();

		count = 0;
		maxCount = max;

		for (int i = 0; i < maxCount; i++)
		{
			GameObject icon = Lean.Pool.LeanPool.Spawn(iconPrefab, transform);
			Vector3 pos = Vector3.zero;
			pos.x = -130f * i;
			icon.transform.localPosition = pos;

			icons.Add(icon);
		}
	}

	private void RemoveIcon()
	{
		GameObject icon = icons[icons.Count - 1];
		Lean.Pool.LeanPool.Despawn(icon);
		icons.Remove(icon);
	}

	public void ClearIcons()
	{
		for (int i = 0; i < icons.Count; i++)
		{
			GameObject icon = icons[i];
			Lean.Pool.LeanPool.Despawn(icon);
		}
		icons.Clear();
	}
}
