﻿namespace ASSPhysics.ChainSystem
{
	public interface IChainElement
	{
		IChainElement chainParent {get;}

		void SetParent (IChainElement parent);	//set this element's parent element. should also add itself to parent childlist
		void AddChild (IChainElement newChild);	//add an element to child list
		IChainElement getChild (int index);		//fetch a child by index
	}
}