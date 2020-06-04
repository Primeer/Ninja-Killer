using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Options", menuName = "Options", order = 51)]
public class Options : ScriptableObject
{
	public float trajectoryDistance;
	public int markerCount;
	public float delayAfterTask;
	public GameObject shurikenPrefab;
	public GameObject markerPrefab;
}
