using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converter
{
	public static Vector3 AngleToDirectionXZ(float angle)
	{
		angle *= Mathf.Deg2Rad;
		float x = Mathf.Sin(angle);
		float z = Mathf.Cos(angle);
		
		return new Vector3(x, 0f, z).normalized;
	}
}
