using Fusion;
using System;
using UnityEngine;

public class PlayerSpwannerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
	[SerializeField] private NetworkPrefabRef playerNetworkPrefab = NetworkPrefabRef.Empty;
	[SerializeField] private Transform[] spwanPoints;

	public override void Spawned()
	{
		if (Runner.IsServer)
		{
			SpwanPlayer(Runner.LocalPlayer);
		}
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.E))
		{
			Runner.Shutdown();
			Debug.Log($"{Runner.LocalPlayer} has left the room..");
		}
	}

	private void SpwanPlayer(PlayerRef playerRef)
	{

		if (Runner.IsServer)
		{
			var index = (int)playerRef.RawEncoded % spwanPoints.Length;
			var spwanPoint = spwanPoints[index].transform.position;
			var playerObject = Runner.Spawn(playerNetworkPrefab, spwanPoint, Quaternion.identity, playerRef);
			Runner.SetPlayerObject(playerRef, playerObject);
		}
	}

	public void PlayerJoined(PlayerRef player)
	{
		if (Runner.IsServer && player != Runner.LocalPlayer)
		{
			SpwanPlayer(player);
		}
	}

	public void PlayerLeft(PlayerRef playerRef)
	{
		DespwanPlayer(playerRef);
	}

	private void DespwanPlayer(PlayerRef playerRef)
	{
		if (Runner.IsServer)
		{
			if (Runner.TryGetPlayerObject(playerRef, out var playerNetworkObject))
			{
				Runner.Despawn(playerNetworkObject);
			}
		}
	}
}
