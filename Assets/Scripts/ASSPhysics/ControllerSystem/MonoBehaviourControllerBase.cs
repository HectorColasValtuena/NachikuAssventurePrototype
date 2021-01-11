using UnityEngine;

namespace ASSPhysics.ControllerSystem
{
	public class MonoBehaviourControllerBase <TController> : MonoBehaviour, IController
		where TController : class, IController
	{
	//MonoBehaviour lifecycle
		public virtual void Awake ()
		{
			//report this controller to the provider
			ControllerProvider.RegisterController<TController>(this);
		}

		public virtual void OnDestroy ()
		{
			ControllerProvider.DisposeController<TController>(this);
			Debug.LogWarning("OnDestroy(): " + typeof(TController));
			Destroy(this);
		}
	//ENDOF MonoBehaviour lifecycle
	}
}