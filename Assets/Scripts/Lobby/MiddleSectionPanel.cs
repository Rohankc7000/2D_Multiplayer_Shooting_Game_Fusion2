using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class MiddleSectionPanel : LobbyPanelBase
{
	[Header("MiddleSectionPanel Variables")]
	[SerializeField] private Button joinRandomRoomBtn;
	[SerializeField] private Button joinRommByArgBtn;
	[SerializeField] private Button createRoomBtn;

	[SerializeField] private TMP_InputField joinRommByArgInputField;
	[SerializeField] private TMP_InputField createRoomInputField;

	private NetworkRunnerController networkRunnerController;

	public override void InitPanel(LobbyUIManager uiManager)
	{
		base.InitPanel(uiManager);
		networkRunnerController = GlobalManagers.Instance.NetworkRunnerController;
		joinRandomRoomBtn.onClick.AddListener(JoinRandomRoom);
		joinRommByArgBtn.onClick.AddListener(() => JoinByArg(GameMode.Client, joinRommByArgInputField.text));
		createRoomBtn.onClick.AddListener(()=>CreateRoom(GameMode.Host,createRoomInputField.text));
	}

	private void CreateRoom(GameMode mode, string roomName)
	{
		if (createRoomInputField.text.Length >= 2)
		{
			Debug.Log($"======={mode.ToString()} Mode =============");
			networkRunnerController.StartGame(mode, roomName);
		}
	}

	private void JoinByArg(GameMode mode, string roomName)
	{
		Debug.Log($"======={mode.ToString()} Mode =============");

		if (joinRommByArgInputField.text.Length >= 2)
		{
			networkRunnerController.StartGame(mode, roomName);
		}
	}

	private void JoinRandomRoom()
	{
		Debug.Log($"=======JoinRandom Mode=============");
		networkRunnerController.StartGame(GameMode.AutoHostOrClient,string.Empty);
	}
}
