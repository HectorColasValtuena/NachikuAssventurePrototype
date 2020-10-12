using Random = UnityEngine.Random;

namespace ASSistant.ASSRandom
{
	public static class RandomSign
	{
		//returns 1 or -1 at 50/50 chance
		public static int Generate ()
		{
			return (Random.Range(0, 2) * 2) - 1;
		}
	}
}