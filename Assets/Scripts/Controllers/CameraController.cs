using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
	private float relativePosition;
	
	
	void Start()
    {
        if(target)
			relativePosition = transform.position.z - target.position.z;
    }

    
    void Update()
    {
        if(target)
		{
			Vector3 newPos = transform.position;
			newPos.z = target.position.z + relativePosition;
			transform.position = newPos;
		}
    }
}
