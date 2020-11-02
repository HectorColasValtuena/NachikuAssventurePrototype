using System.Collections.Generic;
using UnityEngine;

namespace ASSPhysics.ChainSystem
{
	public abstract class ChainElementBase : MonoBehaviour, IChainElement
		//where TChainElement : IChainElement
	{
	//implementación IChainElement
		private List<IChainElement> _chainChildren;
		public IChainElement[] chainChildren { get { return _chainChildren[index]; }}

		public IChainElement chainParent { get; private set; }

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
			if (!_chainChildren.Contains(newChild))
			{
				_chainChildren.Add(newChild);
			}
		}
	//ENDOF implementación IChainElement
	}
}	