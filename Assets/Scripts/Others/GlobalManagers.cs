using UnityEngine;

public class GlobalManagers : MonoBehaviour
{
	public static GlobalManagers Instance { get; private set; }

	[field:SerializeField] public NetworkRunnerController NetworkRunnerController;
	[SerializeField] GameObject parentObj;
	 
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(parentObj);
		}
	}
}
