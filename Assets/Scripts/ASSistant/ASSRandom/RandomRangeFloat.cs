using static UnityEngine.Random;

namespace ASSistant.ASSRandom
{
	[System.Serializable]
	public class RandomRangeFloat
	{
		public float min;
		public float max;

		public RandomRangeFloat (float _min, float _max)
		{
			min = _min;
			max = _max;
		}

		public float Generate ()
		{
			return Random.Range(min, max);
		}
	}
}