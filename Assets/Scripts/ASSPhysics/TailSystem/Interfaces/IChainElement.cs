namespace ASSPhysics.TailSystem
{
	public interface IChainElement<TChainElement>
		where TChainElement : IChainElement<IChainElement>
	{
		TChainElement[] children {get;}
		TChainElement parent {get;}

		void SetParent (TChainElement parent);	//set this element's parent element. should also add itself to parent childlist
		void AddChild (TChainElement newChild);	//add an element to child list
	}
}