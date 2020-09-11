namespace ASSPhysics.HandSystem.Actions
{
	public interface IAction 
	{
		void Initiate (IHand parentHand);
		void Maintain ();
		void Stop ();
	}
}