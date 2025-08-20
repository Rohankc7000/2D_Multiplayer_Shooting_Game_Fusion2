using Fusion;
using Fusion.Sockets;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkRunnerController : MonoBehaviour, INetworkRunnerCallbacks
{
	[SerializeField] private NetworkRunner networkRunnerPrefab;

	private NetworkRunner networkRunnerInstance;


	public async void StartGame(GameMode mode, string roomName)
	{
		if (networkRunnerInstance == null)
		{
			networkRunnerInstance = Instantiate(networkRunnerPrefab);
		}

		networkRunnerInstance.AddCallbacks(this);
		networkRunnerInstance.ProvideInput = true;

		var startGameArgs = new StartGameArgs()
		{
			GameMode = mode,
			SessionName = roomName,
			PlayerCount = 4,
			SceneManager = networkRunnerInstance.GetComponent<INetworkSceneManager>()
		};
		var result = await networkRunnerInstance.StartGame(startGameArgs);
		if (result.Ok)
		{
			const string SCENE_NAME = "MainGame";
			await networkRunnerInstance.LoadScene(SCENE_NAME);
		}
		else
		{
			Debug.LogError(result.ShutdownReason);
		}
	}

	public void OnConnectedToServer(NetworkRunner runner)
	{
		Debug.Log("OnConnectedToServer called");
	}

	public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
	{
		Debug.Log($"OnConnectFailed called | Address: {remoteAddress} | Reason: {reason}");
	}

	public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
	{
		Debug.Log("OnConnectRequest called");
	}

	public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
	{
		Debug.Log("OnCustomAuthenticationResponse called");
	}

	public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
	{
		Debug.Log($"OnDisconnectedFromServer called | Reason: {reason}");
	}

	public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
	{
		Debug.Log("OnHostMigration called");
	}

	public void OnInput(NetworkRunner runner, NetworkInput input)
	{
		Debug.Log("OnInput called");
	}

	public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
	{
		Debug.Log($"OnInputMissing called | Player: {player}");
	}

	public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
	{
		Debug.Log($"OnObjectEnterAOI called | Object: {obj.name} | Player: {player}");
	}

	public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
	{
		Debug.Log($"OnObjectExitAOI called | Object: {obj.name} | Player: {player}");
	}

	public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
	{
		Debug.Log($"OnPlayerJoined called | Player: {player}");
	}

	public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
	{
		Debug.Log($"OnPlayerLeft called | Player: {player}");
	}

	public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
	{
		Debug.Log($"OnReliableDataProgress called | Player: {player} | Key: {key} | Progress: {progress}");
	}

	public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
	{
		Debug.Log($"OnReliableDataReceived called | Player: {player} | Key: {key} | Data Length: {data.Count}");
	}

	public void OnSceneLoadDone(NetworkRunner runner)
	{
		Debug.Log("OnSceneLoadDone called");
	}

	public void OnSceneLoadStart(NetworkRunner runner)
	{
		Debug.Log("OnSceneLoadStart called");
	}

	public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
	{
		Debug.Log($"OnSessionListUpdated called | Session Count: {sessionList.Count}");
	}

	public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
	{
		Debug.Log($"OnShutdown called | Reason: {shutdownReason}");
	}

	public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
	{
		Debug.Log("OnUserSimulationMessage called");
	}
}
