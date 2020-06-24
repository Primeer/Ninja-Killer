using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using GameAnalyticsSDK;

public class GameManager : Singleton<GameManager>
{
	public Player player;
	public ThrowCounter throwCounter;
	[HideInInspector]public Options options;

	public  Task task;
	private List<GameObject> obstacles;
	private Aim aim;
	private WaitForSeconds delayAfterTask;
	private Coroutine afterEmpty;

	public override void Awake() {
		base.Awake();
		options = Resources.Load<Options>("Options");
		aim = GetComponent<Aim>();
		delayAfterTask = new WaitForSeconds(options.delayAfterTask);
		player = FindObjectOfType<Player>();
		throwCounter = FindObjectOfType<ThrowCounter>();
		obstacles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Obstacle"));

		GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"Level {SceneManager.GetActiveScene().buildIndex + 1}");
	}

	private void OnEnable() {
		InputSystem.onMainButtonTap += AimStart;
		InputSystem.onMainButtonDrag += Aim;
		InputSystem.onMainButtonRelease += AimEnd;

		// ShurikenSpawner.onShurikenDespawn += CheckTask;
		ShurikenSpawner.onShurikenDespawn += () => InputSystem.Instance.MainButtonEnabled(true);
	}

	private void OnDisable() {
		InputSystem.onMainButtonTap -= AimStart;
		InputSystem.onMainButtonDrag -= Aim;
		InputSystem.onMainButtonRelease -= AimEnd;

		// ShurikenSpawner.onShurikenDespawn -= CheckTask;
		ShurikenSpawner.onShurikenDespawn -= () => InputSystem.Instance.MainButtonEnabled(true);
	}

	public void AimStart(float angle)
	{
		aim.SetAngle(angle);
	}

	public void Aim(float angle)
	{
		aim.SetAngle(angle);
	}

	public void AimEnd(float angle)
	{
		InputSystem.Instance.MainButtonEnabled(false);
		
		aim.SetAngle(angle);
		player.Throw(angle);
		aim.Hide();

		throwCounter.Count();
	}

	public void SetTask(Obstacle obs)
	{
		if (task != null)
		{
			task.onComplete -= OnTaskComplete;
			task.onFail -= OnTaskFail;
		}

		task = new Task(obs);
		task.onComplete += OnTaskComplete;
		task.onFail += OnTaskFail;

		OnTaskBegin();
	}

	public void OnTaskBegin()
	{
		InputSystem.Instance.MainButtonEnabled(true);
		player.Stop();

		throwCounter.ResetCount(task.shurikenCount);
	}

	public void OnShurikenEmpty()
	{
		if(task == null) return;

		afterEmpty = StartCoroutine(AfterEmpty());
	}

	public void OnTaskComplete()
	{
		if(afterEmpty != null)
			StopCoroutine(afterEmpty);
		
		InputSystem.Instance.MainButtonEnabled(false);
		throwCounter.ClearIcons();

		obstacles.Remove(task.obstacle.gameObject);	
		task = null;

		StartCoroutine(AfterTaskComplete());
	}

	public void OnTaskFail()
	{
		StartCoroutine(AfterTaskFail());
	}

	public void RestartLevel()
	{
		GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"Level {SceneManager.GetActiveScene().buildIndex + 1}");

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Awake();
	}

	public void LoadNextLevel()
	{
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

		GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Level {SceneManager.GetActiveScene().buildIndex + 1}");
		
		if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
			nextSceneIndex = 0;
		
		SceneManager.LoadScene(nextSceneIndex);
		Awake();
	}

	private IEnumerator AfterTaskComplete()
	{
		yield return delayAfterTask;
		
		player.Move();

		if(obstacles.Count == 0)
			LoadNextLevel();
	}

	private IEnumerator AfterTaskFail()
	{
		yield return delayAfterTask;
		RestartLevel();
	}

	private IEnumerator AfterEmpty()
	{
		yield return new WaitForSeconds(task.delayAfterTask);
		RestartLevel();
	}
}
