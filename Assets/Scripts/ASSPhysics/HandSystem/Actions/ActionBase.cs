namespace ASSPhysics.HandSystem.Actions
{
	public abstract class ActionBase : IAction
	{
	//IHandAction implementation
		public void Initiate (ITool parentTool)
		{
			tool = parentTool;
			PerformInitiate();
		}
		public abstract void Maintain ();
		public abstract void Stop ();
	//ENDOF IHandAction implementation

		protected ITool tool;

		protected abstract void PerformInitiate ();
	}
}