using UnityEngine;

namespace ASSistant.ASSMath
{
	//methods for Rect manipulation
	public static class Vector3Math
	{
	//Vector3 creation methods
		public static Vector3 AngleToVector3 (float angle)
		{
			return new Vector3 (Mathf.Sin(angle), Mathf.Cos(angle), 0);
		}
	//ENDOF Vector3 creation methods
	}
}