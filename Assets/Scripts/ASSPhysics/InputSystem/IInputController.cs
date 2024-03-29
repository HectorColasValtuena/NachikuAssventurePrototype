﻿using Vector3 = UnityEngine.Vector3;

namespace ASSPhysics.InputSystem
{
	public interface IInputController : ASSPhysics.ControllerSystem.IController
	{
		//input movement delta for last frame
		Vector3 delta { get; }
		Vector3 screenSpaceDelta { get; }

		//scaled delta for configurable controls
		Vector3 scaledDelta { get; }

		//gets zoom input
		float zoomDelta { get; }

		//gets button pressed
		bool GetButtonDown (int buttonID);

	}
}