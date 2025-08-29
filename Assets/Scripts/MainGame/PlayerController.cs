using Fusion;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
	[SerializeField] private GameObject cam;
	[SerializeField] private float moveSpeed = 6;
	[SerializeField] private float JumpForce = 10;
	private float horizontal;
	private Rigidbody2D rb;

	[Networked] private NetworkButtons prevButtons { get; set; }

	private enum PlayerInputButtons
	{
		None,
		Jump
	}

	public override void Spawned()
	{
		rb = GetComponent<Rigidbody2D>();
		SetLocalObjects();
	}

	public override void FixedUpdateNetwork()
	{
		if (Runner.TryGetInputForPlayer<PlayerData>(Object.InputAuthority, out var input))
		{
			rb.linearVelocity = new Vector2(input.HorizontalInput * moveSpeed, rb.linearVelocity.y);
			CheckJump(input);
		}
		prevButtons = input.NetworkButtons;
	}

	private void SetLocalObjects()
	{
		if (Runner.LocalPlayer == Object.InputAuthority)
		{
			cam.SetActive(true);
		}
	}

	public void BeforeUpdate()
	{
		if (Runner.LocalPlayer == Object.InputAuthority)
		{
			const string HORIZONTAL = "Horizontal";
			horizontal = Input.GetAxis(HORIZONTAL);
		}
	}

	private void CheckJump(PlayerData input)
	{
		Debug.Log("Checking the jumps");
		var pressed = input.NetworkButtons.GetPressed(prevButtons);
		if (pressed.WasPressed(prevButtons, PlayerInputButtons.Jump))
		{
			Debug.Log("Inside  the jumps");
			rb.AddForce(Vector3.up * JumpForce, ForceMode2D.Force);
		}
	}

	public PlayerData GetPlayerNetworkInput()
	{
		PlayerData data = new PlayerData();
		data.HorizontalInput = horizontal;
		data.NetworkButtons.Set(PlayerInputButtons.Jump, Input.GetKey (KeyCode.Space));
		return data;
	}
}
