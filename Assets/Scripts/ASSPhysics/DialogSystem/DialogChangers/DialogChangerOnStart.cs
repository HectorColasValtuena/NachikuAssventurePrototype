namespace ASSPhysics.DialogSystem
{
	public class DialogChangerOnStart : DialogChangerBase
	{
	//serialized fields
		[UnityEngine.SerializeField]
		float delay = 0.0f;
	//ENDOF serialized fields

	//public methods
		public void Start ()
		{
			StartCoroutine(DelayedChangeDialog());
		}
	//ENDOF public methods

	//private methods
		private System.Collections.IEnumerator DelayedChangeDialog ()
		{
			yield return new UnityEngine.WaitForSeconds(delay);
			ChangeDialog();
		}
	//ENDOF private methods
	}
}