using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public Player player;
	[HideInInspector]public Options options;

	public  Task task;
	private List<GameObject> obstacles;
	private Trajectory trajectory;
	private WaitForSeconds delay;

	public override void Awake() {
		base.Awake();
		options = Resources.Load<Options>("Options");
		trajectory = GetComponent<Trajectory>();
		// delay = new WaitForSeconds(options.delayAfterTask);
		player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Player>();
		obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));
	}

	private void OnEnable() {
		InputSystem.onMainButtonTap += AimStart;
		InputSystem.onMainButtonDrag += Aim;
		InputSystem.onMainButtonRelease += AimEnd;

		ShurikenSpawner.onShurikenDespawn += CheckTask;
	}

	private void OnDisable() {
		InputSystem.onMainButtonTap -= AimStart;
		InputSystem.onMainButtonDrag -= Aim;
		InputSystem.onMainButtonRelease -= AimEnd;

		ShurikenSpawner.onShurikenDespawn -= CheckTask;
	}

	public void AimStart(float angle)
	{
		trajectory.Set(angle);
	}

	public void Aim(float angle)
	{
		trajectory.Set(angle);
	}

	public void AimEnd(float angle)
	{
		InputSystem.Instance.MainButtonEnabled(false);
		
		trajectory.Set(angle);
		player.Throw(angle);
		trajectory.Hide();

		ThrowCounter.Instance.Count();
	}

	public void SetTask(Obstacle obs)
	{
		task = new Task(obs);
		OnTaskBegin();
	}

	public void OnTaskBegin()
	{
		InputSystem.Instance.MainButtonEnabled(true);
		player.Stop();

		ThrowCounter.Instance.ResetCount(task.shurikenCount);

		task.active = true;
	}

	public void CheckTask()
	{
		if(task.completed)
			OnTaskComplete();
		else if(!ThrowCounter.Instance.Empty)
			InputSystem.Instance.MainButtonEnabled(true);
		else OnTaskFail();
	}

	public void OnTaskComplete()
	{
		InputSystem.Instance.MainButtonEnabled(false);
		ThrowCounter.Instance.ClearIcons();

		obstacles.Remove(task.obstacle.gameObject);
		task.active = false;
		task = null;

		player.Move();

		if(obstacles.Count == 0)
			NextLevel();
	}

	public void OnTaskFail()
	{
		RestartLevel();
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		Awake();
	}

	public void NextLevel()
	{
		if (SceneManager.GetActiveScene().buildIndex + 1 != SceneManager.sceneCountInBuildSettings)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			Awake();
		} 
		else player.Stop();
	}
}
