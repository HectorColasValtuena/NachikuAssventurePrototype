using System.Collections.Generic;
using UnityEngine;

namespace ASSPhysics.ChainSystem
{
	public abstract class ChainElementBase : MonoBehaviour, IChainElement
		//where TChainElement : IChainElement
	{
	//implementación IChainElement
		private List<IChainElement> chainChildren;
		public IChainElement chainParent { get; private set; }

		public int childCount { get { return chainChildren.Count; }}

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
			if (chainChildren == null) chainChildren = new List<IChainElement>();
			if (!chainChildren.Contains(newChild))
			{
				chainChildren.Add(newChild);
			}
		}

		//fetch a child by index
		public IChainElement GetChild (int index)
		{
			return chainChildren[index];
		}
	//ENDOF implementación IChainElement
	}
}	