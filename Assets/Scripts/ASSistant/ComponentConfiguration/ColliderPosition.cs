using UnityEngine;

using Type = System.Type;
using System.Reflection;	//PropertyInfo

namespace ASSistant.ComponentConfiguration
{
	public static class ColliderPosition
	{
		private const string offsetPropertyName = "center";
		private static readonly BindingFlags defaultBindingFlags =
			BindingFlags.Instance |
			BindingFlags.Public |
			BindingFlags.SetProperty |
			BindingFlags.GetProperty;

		//extension method letting a sphere collider report its worldspace position
		//considers its offset property if it has one
		public static Vector3 EMGetColliderAbsolutePosition (this Collider collider)
		{
			//return collider's transform position adding offset value if available
			return collider.transform.position + collider.EMGetColliderTransformOffset();
		}

		public static Vector3 EMGetColliderTransformOffset (this Collider collider)
		{
			PropertyInfo offsetProperty = collider
				.GetType()					//fetch received collider's type signature
				.GetProperty(offsetPropertyName, defaultBindingFlags);	//try to fetch offset value property

			return (offsetProperty != null
						? (Vector3) offsetProperty.GetValue(collider)
						: Vector3.zero);
		}
	}
}