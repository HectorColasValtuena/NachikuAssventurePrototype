namespace ASSPhysics.TailSystem
{
	public interface ITailElement
	{
		float offsetRotation {get; set;}
		ITailElement[] childElementList {get;}
	}
}