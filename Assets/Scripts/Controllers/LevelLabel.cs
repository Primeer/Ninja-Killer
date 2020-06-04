using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLabel : MonoBehaviour
{
	[SerializeField] private float showingTime;
	private WaitForSeconds time;

	void Start()
    {
		time = new WaitForSeconds(showingTime);
		StartCoroutine(Show());
	}

    private IEnumerator Show()
	{
		yield return time;
		Destroy(gameObject);
	}
}
