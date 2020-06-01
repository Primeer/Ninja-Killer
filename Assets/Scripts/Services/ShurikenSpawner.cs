﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class ShurikenSpawner : MonoBehaviour
{
	public delegate void Method();
	public static event Method onShurikenDespawn;

	public static void Spawn(float angle)
	{
		GameObject shuriken = LeanPool.Spawn(GameManager.Instance.options.shurikenPrefab);
		shuriken.GetComponent<Shuriken>().Init(angle);
	}

	public static void Despawn(GameObject shuriken)
	{
		LeanPool.Despawn(shuriken);

		if(onShurikenDespawn != null)
			onShurikenDespawn();
	}
}
