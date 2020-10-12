using Random = UnityEngine.Random;

namespace ASSistant
{
	public static class AssMath
	{
		//returns 1 or -1 at 50/50 chance
		public static int RandomSign ()
		{
			return (Random.Range(0, 2) * 2) - 1;
		}
	}
}