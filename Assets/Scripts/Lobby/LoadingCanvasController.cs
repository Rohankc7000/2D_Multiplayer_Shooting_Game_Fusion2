using System;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCanvasController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private Button cancelButton;
	private NetworkRunnerController networkRunnerController;


	private void Start()
	{
		networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
		networkRunnerController.OnStartedRunnerConnection += OnStartedRunnerConnection;
		networkRunnerController.OnPlayerJoinedSuccessfully += OnPlayerJoinedSuccessfullly;

		cancelButton.onClick.AddListener(networkRunnerController.ShutDownRunner);

		this.gameObject.SetActive(false);
	}

	private void OnPlayerJoinedSuccessfullly()
	{
		const string CLIP_NAME = "OUT";
		Utilis.PlayAnimationSetStateWhenFinished(gameObject, animator, CLIP_NAME, false);
	}

	private void OnStartedRunnerConnection()
	{
		this.gameObject.SetActive(true);
		const string CLIP_NAME = "In";
		Utilis.PlayAnimationSetStateWhenFinished(gameObject, animator, CLIP_NAME);
	}


	private void OnDestroy()
	{
		networkRunnerController.OnStartedRunnerConnection -= OnStartedRunnerConnection;
		networkRunnerController.OnPlayerJoinedSuccessfully -= OnPlayerJoinedSuccessfullly;
	}
}
