namespace ASSPhysics.DialogSystem
{
	public abstract class DialogChangerOnConditionBase : DialogChangerBase
	{
	//MonoBehaviour lifecycle
		public void Update ()
		{
			if (CheckCondition())
			{
				ChangeDialog();
			}
		}
	//ENDOF MonoBehaviour lifecycle

	//abstract method declaration
		//this method should return true when the condition for dialog change is fulfilled
		protected abstract bool CheckCondition ();
	//ENDOF abstract method declaration
	}
}