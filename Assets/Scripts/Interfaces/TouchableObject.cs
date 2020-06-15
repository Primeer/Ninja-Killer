using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableObject : MonoBehaviour
{
    [SerializeField] private Type type;
    [SerializeField] private bool solid;
	public bool Solid { get { return solid; } }
	private enum Type
	{
		Static,
		Dynamic
	}

	private void OnCollisionEnter(Collision other) {
		TouchableObject touchObj = other.gameObject.GetComponentInParent<TouchableObject>();
		if(touchObj)
			touchObj.Touch(other.impulse);
	}

	public virtual void Touch(Vector3 force)
	{
		if(type != Type.Dynamic) return;
		
		Rigidbody rig = GetComponentInChildren<Rigidbody>();
		rig.isKinematic = false;
		rig.AddForce(force);
	}
}
