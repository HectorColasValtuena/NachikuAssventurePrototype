using System.Collections.Generic;
using UnityEngine;

namespace ASSPhysics.ChainSystem
{
	public abstract class ChainElementBase : MonoBehaviour, IChainElement
	{
	//private fields
		private List<IChainElement> _chainChildren;

		private IChainElement _chainParent;
	//ENDOF serialized fields

	//implementación IChainElement
		public IChainElement chainParent
		{
			get { return _chainParent; }
			private set { _chainParent = value; }
		}

		public int childCount { get { return (_chainChildren != null) ? _chainChildren.Count : 0; }}

		//set this element's parent element. Also adds itself as its parent's child
		public void SetParent (IChainElement newParent)
		{
			chainParent = newParent;
			if (newParent != null)
			{
				newParent.AddChild(this);
			}
		}

		//add an element to child list
		public void AddChild (IChainElement newChild)
		{
			if (_chainChildren == null) _chainChildren = new List<IChainElement>();
			if (!_chainChildren.Contains(newChild))
			{
				_chainChildren.Add(newChild);
			}
		}

		//fetch a child by index
		public IChainElement GetChild (int index)
		{
			return _chainChildren[index];
		}
	//ENDOF implementación IChainElement
	}
}	