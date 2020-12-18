using UnityEngine;

namespace ASSistant.ASSMath
{
	//methods for Rect manipulation
	public static class RectMath
	{
	//Rect creation methods
		//Creates a new rect with given dimensions at target position
		public static Rect RectFromCenterAndSize (Vector3 position, Vector3 size)
		{ return RectFromCenterAndSize(position, size.x, size.y); }
		public static Rect RectFromCenterAndSize (Vector3 position, Vector2 size)
		{ return RectFromCenterAndSize(position, size.x, size.y); }
		public static Rect RectFromCenterAndSize (Vector3 position, float width, float height)
		{
			return new Rect(
				x: position.x - (width / 2),
				y: position.y - (height / 2),
				width: width,
				height: height
			);
		}
	//ENDOF Rect creation methods

	//Rect clamping and trimming methods 
		//clamp a x/y position within a rect
		public static Vector2 ClampVector2WithinRect (Vector2 position, Rect outerRect)
		{
			return new Vector2
			(
				x: Mathf.Clamp(position.x, outerRect.xMin, outerRect.xMax),
				y: Mathf.Clamp(position.y, outerRect.yMin, outerRect.yMax),
			);
		}
		public static Vector3 ClampVector3WithinRect (Vector3 position, Rect outerRect)
		{
			return new Vector3
			(
				x: Mathf.Clamp(position.x, outerRect.xMin, outerRect.xMax),
				y: Mathf.Clamp(position.y, outerRect.yMin, outerRect.yMax),
				z: position.z
			);
		}

		//ensures innerRect bounds stay within outerRect by moving innerRect if protruding.
		//if innerRect dimensions exceed outerRect, they will be centered
		public static Rect ClampRectPositionToRect (Rect innerRect, Rect outerRect)
		{
			return new Rect (
				x: (innerRect.width <= outerRect.width)
					? //if innerRect is thinner than outerRect, clamp its position within outerRect
						Mathf.Clamp(					
							value: innerRect.x,
							min: outerRect.xMin,
							max: outerRect.xMax - innerRect.width
						)
					: //if innerRect is wider than outerRect, center their position
						outerRect.x - ((innerRect.width - outerRect.width) / 2),
				y: (innerRect.height <= outerRect.height)
					? //if innerRect is shorter than outerRect clamp its position
						Mathf.Clamp(
							value: innerRect.y,
							min: outerRect.yMin,
							max: outerRect.yMax - innerRect.height
						)
					: //if innerRect is taller than outerRect, center their position
						innerRect.y - ((innerRect.height - outerRect.width) / 2),
				width: innerRect.width,
				height: innerRect.height
			);
		}

		//truncates innerRect dimensions to fit outerRect. may return the same rect if already small enough.
		//only alters size, returned rect's position will be the same as innerRect's
		public static Rect TrimRectSizeToRect (Rect innerRect, Rect outerRect)
		{
			if (innerRect.width <= outerRect.width && innerRect.height <= outerRect.height)
			{ return innerRect; }
			return new Rect (
				x: innerRect.x,
				y: innerRect.y,
				width: Mathf.Clamp(innerRect.width, 0, outerRect.width),
				height: Mathf.Clamp(innerRect.height, 0, outerRect.height)
			);
		}
	//ENDOF Rect clamping and trimming methods
	}
}