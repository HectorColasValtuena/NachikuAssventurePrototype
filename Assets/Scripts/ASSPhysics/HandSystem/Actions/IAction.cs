namespace ASSPhysics.HandSystem.Actions
{
	public interface IAction 
	{
		void Initiate (ITool parentTool);
		void Maintain ();
		void Stop ();
	}
}