using UnityEngine;

using static ASSPhysics.HandSystem.Mouse.MouseInput;

namespace ASSPhysics.HandSystem
{
	public class HandManagerFromMouseCursor : MonoBehaviour
	{
		[SerializeField]
		protected IHand[] handList;	//list of hands

		private int mainHand;		//highligted and active hand

		public void Update ()
		{
			HandCycleCheck();
			UpdateMainHand();
			MainActionCheck();
		}

		//checks for input corresponding to hand swap, and performs the action if necessary
		private void HandCycleCheck ()
		{
			
		}

		//
		private void HandCyclePerform ()
		{
			
		}

		private void UpdateMainHand ()
		{

		}

		//checks for input corresponding to main action (grab/slap), and performs the action if necessary
		private void MainActionCheck ()
		{

		}

		private void MainActionPerform ()
		{

		}
	}
}