using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	private Animator animator;
	private ThrowingAnimation throwingAnimation;

	private void Awake() {
		animator = GetComponent<Animator>();
		throwingAnimation = GetComponent<ThrowingAnimation>();
	}

	private void Start() {
		Move();
	}

	private void OnTriggerEnter(Collider other) {
		TaskTrigger trigger = other.GetComponent<TaskTrigger>();

		if(trigger)
			if(GameManager.Instance.task == null)
				GameManager.Instance.SetTask(trigger.Obstacle);
	}

	public void Throw(float angle)
	{
		throwingAnimation.Begin(angle);
	}

	public void Move()
	{
		animator.ResetTrigger("stop");
		animator.SetTrigger("run");
	}

	public void Stop()
	{
		animator.ResetTrigger("run");
		animator.SetTrigger("stop");
	}
}
