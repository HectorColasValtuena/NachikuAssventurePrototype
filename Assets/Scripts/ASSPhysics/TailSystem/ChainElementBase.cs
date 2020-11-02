using System.Collections.Generic;

namespace ASSPhysics.TailSystem
{
	public abstract class ChainElementBase<TChainElement> : MonoBehaviour, IChainElement<TChainElement>
		where TChainElement : IChainElement<IChainElement>
	{
	//implementación IChainElement
		private List<TChainElement> _childList;
		public TChainElement[] children { get { return _childList[index]; }}

		private TChainElement _parent;
		public TChainElement parent {
			get { return _parent; }
			private set { _parent = value; }
		}

		//set this element's parent element. Also adds itself as its parent's child
		public void SetParent (TChainElement newParent)
		{
			parent = newParent;
			if (newParent != null)
			{
				newParent.AddChild(this);
			}
		}

		//add an element to child list
		public void AddChild (TChainElement newChild)
		{
			//if (!)
		}
	//ENDOF implementación IChainElement
	}
}	