using Random = UnityEngine.Random;

namespace ASSistant.ASSRandom
{
	[System.Serializable]
	public class RandomRangeInt
	{
		public int min;
		public int max;

		public RandomRangeInt (int _min, int _max)
		{
			min = _min;
			max = _max;
		}

		public int Generate ()
		{
			return Random.Range(min, max);
		}
	}
}