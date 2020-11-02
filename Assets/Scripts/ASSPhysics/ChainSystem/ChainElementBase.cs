using System.Collections.Generic;
using UnityEngine;

namespace ASSPhysics.ChainSystem
{
	public abstract class ChainElementBase : MonoBehaviour, IChainElement
		//where TChainElement : IChainElement
	{
	//implementación IChainElement
		private List<IChainElement> _childList;
		public IChainElement[] chainChildren { get { return _childList[index]; }}

		private IChainElement _parent;
		public IChainElement chainParent
		{
			get { return _parent; }
			private set { _parent = value; }
		}

		//set this element's parent element. Also adds itself as its parent's child
		public void SetParent (IChainElement newParent)
		{
			parent = newParent;
			if (newParent != null)
			{
				newParent.AddChild(this);
			}
		}

		//add an element to child list
		public void AddChild (IChainElement newChild)
		{
			//if (!)
		}
	//ENDOF implementación IChainElement
	}
}	