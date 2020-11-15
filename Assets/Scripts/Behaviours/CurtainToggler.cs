using UnityEngine;

using CurtainsController = ASSPhysics.SceneSystem.CurtainsController;

public class CurtainToggler : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			CurtainsController.open = true;
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			CurtainsController.open = false;
		}
	}
}
