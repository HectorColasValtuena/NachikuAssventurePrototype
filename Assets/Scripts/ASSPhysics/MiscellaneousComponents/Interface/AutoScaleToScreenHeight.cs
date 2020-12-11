using UnityEngine;

using ControllerCache = ASSPhysics.ControllerSystem.ControllerCache;

namespace ASSPhysics.MiscellaneousComponents
{
	public class AutoScaleToScreenHeight : MonoBehaviour
	{
	//serialized fields
		[SerializeField]
		private float baseScreenHeight = 1.0f;
		[SerializeField]
		private Vector3 baseScale = Vector3.one;
	//ENDOF serialized fields

	//MonoBehaviour lifecycle
		public void Start () 
		{
			UpdateScale();
		}

		public void Update ()
		{
			UpdateScale();
		}
	//ENDOF MonoBehaviour lifecycle

	//private methods
		private void UpdateScale()
		{ 	
			transform.localScale = baseScale * (ControllerCache.viewportController.viewportHeight / baseScreenHeight);
		}
	//ENDOF private methods
	}
}