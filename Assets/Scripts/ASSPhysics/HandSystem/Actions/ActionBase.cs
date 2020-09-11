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
		public abstract void Maintain ();
		public abstract void Stop ();
	//ENDOF IHandAction implementation

		protected IHand hand;

		protected abstract void PerformInitiate ();
	}
}