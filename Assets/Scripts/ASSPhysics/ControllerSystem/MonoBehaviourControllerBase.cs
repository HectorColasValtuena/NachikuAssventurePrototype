using UnityEngine;

namespace ASSPhysics.ControllerSystem
{
	public class MonoBehaviourControllerBase <TController> : MonoBehaviour, IController
		where TController : class, IController
	{
	//IController implementation
		private bool _isAlive;
		public bool isValid
		{
			get { return _isAlive; }
			private set { _isAlive = value; }
		}
	//ENDOF IController implementation

	//MonoBehaviour lifecycle
		public virtual void Awake ()
		{
			//report this controller to the provider
			isValid = true;
			ControllerProvider.RegisterController<TController>(this);
		}

		public virtual void OnDestroy ()
		{
			ControllerProvider.DisposeController<TController>(this);
			Debug.LogWarning("OnDestroy(): " + typeof(TController));
			isValid = false;
			Destroy(this);
		}
	//ENDOF MonoBehaviour lifecycle
	}
}