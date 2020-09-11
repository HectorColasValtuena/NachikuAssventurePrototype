namespace ASSPhysics.HandSystem.Actions
{
	public interface IAction 
	{
		public void Initiate (IHand parentHand);
		public void Maintain ();
		public void Finalize ();
	}
}