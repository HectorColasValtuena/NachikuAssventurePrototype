//using System.Collections;
//using System.Collections.Generic;
//using static System.Type;

using UnityEngine;

namespace ASSPhysics.TailSystem
{
	public class TailWiggleParentTransform : TailWiggleParentBase
	{
		public override System.Type GetElementType()
		{
			return typeof (TailWiggleElementTransform);
		}
	}
}