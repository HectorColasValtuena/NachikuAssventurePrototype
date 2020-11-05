using UnityEngine;

namespace ASSPhysics.ChainSystem
{
	public abstract class ChainElementAutoFindParent : ChainElementBase
	{
	//MonoBehaviour lifecycle implementation
		public virtual void Awake ()
		{
			SetParent(transform.parent.GetComponent<IChainElement>());
		}
	//ENDOF MonoBehaviour lifecycle implementation
	}
}	