namespace ASSPhysics.HandSystem.Actions
{
	public abstract class ActionBase : IAction
	{
	//IHandAction implementation
		public void Initiate (IHand parentHand)
		{
			hand = parentHand;
			PerformInitiate();
		}
		public virtual void Maintain ();
		public virtual void Finalize ();
	//ENDOF IHandAction implementation

		private IHand hand;

		private virtual void PerformInitiate ();
	}
}