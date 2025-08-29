using Fusion;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
	[SerializeField] private Camera mainCamera;

	public override void Spawned()
	{
		mainCamera.gameObject.SetActive(false);
	}
}
