namespace ASSPhysics.MiscellaneousComponents.Kickers
{
	//interface implemented by every kicker component
	public interface IKicker
	{
		void Kick();	//applies target physics force once (a kick)
	}
}