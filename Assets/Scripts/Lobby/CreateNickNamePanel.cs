using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateNickNamePanel : LobbyPanelBase
{

	[Header("CreateNickNamePanel Variables")]
	[SerializeField] private TMP_InputField inputField;
	[SerializeField] private Button createNickNameButton;
	private const int MAX_CHAR_FOR_NICKNAME = 2;

	public override void InitPanel(LobbyUIManager uiManager)
	{
		base.InitPanel(uiManager);
		createNickNameButton.onClick.AddListener(OnClickNickName);
		inputField.onValueChanged.AddListener(OnInputValueChanged);
	}

	private void OnInputValueChanged(string arg0)
	{
		createNickNameButton.interactable = arg0.Length >= MAX_CHAR_FOR_NICKNAME;
	}


	private void OnClickNickName()
	{
		var nickName = inputField.text;
		if (!string.IsNullOrEmpty(nickName) && nickName.Length >= MAX_CHAR_FOR_NICKNAME)
		{
			base.ClosePanel();
			lobbyUIManager.ShowPanel(LobbyPanelType.MiddleSectionPanel);
		}
	}
}
