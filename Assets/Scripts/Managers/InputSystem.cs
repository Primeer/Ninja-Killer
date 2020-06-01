using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : Singleton<InputSystem>
{
	public delegate void InputMethod(float angle);
	public static event InputMethod onMainButtonTap;
	public static event InputMethod onMainButtonDrag;
	public static event InputMethod onMainButtonRelease;


	[SerializeField] private float maxAngle;
	[SerializeField] private Mode mode;
	private enum Mode
	{
		First,
		Second
	}

	private Transform player;

	private bool isMainButton; 

	private void Start() {
		player = GameManager.Instance.player.transform;
	}

	public void MainButtonTap(Vector2 pos)
	{
		if(!isMainButton) return;

		if (onMainButtonTap != null)
			onMainButtonTap(GetDiviationAngle(pos));
	}

	public void MainButtonDrag(Vector2 pos)
	{
		if(!isMainButton) return;

		if (onMainButtonTap != null)
			onMainButtonDrag(GetDiviationAngle(pos));
	}

	public void MainButtonRelease(Vector2 pos)
	{
		if(!isMainButton) return;

		if (onMainButtonTap != null)
			onMainButtonRelease(GetDiviationAngle(pos));
	}

	public void MainButtonEnabled(bool flag) => isMainButton = flag;

	private float GetDiviationAngle(Vector3 pos)
	{
		switch (mode)
		{
			case Mode.First:
				return GetProjectionAngle(pos);
			case Mode.Second:
				return GetСorrespondingAngle(pos);
		}
		return 0f;
	}

	private float GetProjectionAngle(Vector2 pos)
	{
		Vector3 worldPos = Vector3.zero;

		Ray ray = Camera.main.ScreenPointToRay((Vector3)pos);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Default")))
			worldPos = hit.point;

		worldPos.y = player.position.y;
		Vector3 dir = worldPos - player.position;

		float angle = Vector3.Angle(Vector3.forward, dir);
		angle = angle > maxAngle ? maxAngle : angle;

		return worldPos.x >= player.position.x ? angle : -angle;
	}

	private float GetСorrespondingAngle(Vector2 pos)
	{
		int mid = Screen.width / 2;

		float diff = pos.x - mid;

		float part = diff / mid;

		float angle = part * 90f;
		angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

		return angle;
	}
}
