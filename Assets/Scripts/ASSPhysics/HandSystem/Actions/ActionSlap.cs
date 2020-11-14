using UnityEngine;

using ActionSettings = ASSPhysics.SettingSystem.ActionSettings; //
using EInputState = ASSPhysics.InputSystem.EInputState;

namespace ASSPhysics.HandSystem.Actions
{
	public class ActionSlap : ActionBase
	{
	//ActionBase override implementation
		//receive state of corresponding input medium
		public override void Input (EInputState state)
		{
			if (state == EInputState.Ended)
			{
				PerformSlap();
			}
		}

		//interaction is always valid - slap is generally the fallback state if no othe action is valid
		public override bool IsValid ()
		{
			return true;
			//alternative implementation determining wether there were valid colliders in range
			//return ActionSettings.slapAreaSettings.GetCollidersInRange(tool.transform).Length > 0;
		}

		//Using an interactor is an entirely non-automatable one-shot action, so automation methods just report failure
		public override bool Automate () { return false; }
		public override bool AutomationUpdate () { return false; }
		//public override void DeAutomate ();
	//ENDOF ActionBase override implementation

	//private method implementation
		//execute the slapping action
		private void PerformSlap ()
		{
			//fetch colliders in range around the tool
			Collider[] colliderList = ActionSettings.slapAreaSettings.GetCollidersInRange(tool.transform);
			//add a force to each collider in range
			foreach (Collider collider in colliderList)
			{
				////////////////////////////////////////////////////////////////////
				//[TO-DO] move explosionForce to an actionSetting
				const float explosionForce = 40.0f;
				//////////////////////
				Rigidbody targetRigidbody = collider.GetComponent<Rigidbody>();

				if(targetRigidbody != null) {
					targetRigidbody.AddExplosionForce(
						explosionForce: explosionForce,	//float
						explosionPosition: tool.transform.position,	//Vector3
						explosionRadius: ActionSettings.slapAreaSettings.radius,	//float
						upwardsModifier: 0.0f,	//float
						mode: ForceMode.Force	//ForceMode
					);
				}
			}
		}
	//ENDOF private method implementation
	}
}