using System.Collections.Generic;
using UnityEngine;

namespace ASSPhysics.ChainSystem
{
	public abstract class ChainElementBase : MonoBehaviour, IChainElement
		//where TChainElement : IChainElement
	{
	//serialized fields
		//[SerializeField]
		public List<IChainElement> m_chainChildren;

		//[SerializeField]
		public IChainElement m_chainParent;
	//ENDOF serialized fields

	//implementación IChainElement
		public IChainElement chainParent
		{
			get { return m_chainParent; }
			private set { m_chainParent = value; }
		}

		public int childCount { get { return (m_chainChildren != null) ? m_chainChildren.Count : 0; }}

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
			if (m_chainChildren == null) m_chainChildren = new List<IChainElement>();
			if (!m_chainChildren.Contains(newChild))
			{
				m_chainChildren.Add(newChild);
			}
		}

		//fetch a child by index
		public IChainElement GetChild (int index)
		{
			return m_chainChildren[index];
		}
	//ENDOF implementación IChainElement
	}
}	