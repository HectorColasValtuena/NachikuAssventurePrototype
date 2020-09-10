using UnityEngine;

namespace ASSPhysics.HandSystem
{
	public class Hand : MonoBehaviour, IHand
	{
		private const float minimumTimeHeldForGrab = 0.1f;
		private const float maximumTimeHeldForSlap = 0.3f;

	//IHand implementation
		public Vector3 targetPosition { set { _targetPosition = value; }}

		//=============================================================================
		//[TO-DO]
		//=============================================================================
		//called upon pressing, holding, and releasing input button
		public void MainInput (EInputState state)
		{
			switch (state)
			{
				case EInputState.Started:
					InputStarted();
				case EInputState.Held:
					InputHeld();
					break;
				case EInputState.Ended:
					InputEnded();
					break;
			}
		}
	//ENDOF IHand implementation

		private Vector3 _targetPosition = Vector3.zero;
		private float inputHeldTime = 0.0f;

		private void InputStarted ()
		{
			inputHeldTime = 0.0f;
		}

		private void InputHeld ()
		{

		}

		private void InputEnded ()
		{
			
		}

		public void Awake ()
		{

		}

		public void Update()
		{
			
		}

		public void Slap (){}
		public void Grab (){}
	}
}
