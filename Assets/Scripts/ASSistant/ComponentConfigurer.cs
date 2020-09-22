using System;	//Type.GetProperties
using System.Reflection; //BindingFlags

using UnityEngine;

namespace ASSistant.ComponentConfiguration
{
	public static class ComponentConfigurer
	{
		//default binding flags limit properties read/written to:
		//Non-Static, public properties with both set and get methods
		private static readonly BindingFlags defaultBindingFlags =
			BindingFlags.Instance |
			BindingFlags.Public |
			BindingFlags.SetProperty |
			BindingFlags.GetProperty;


		//applies right-hand properties to left-hand objects. returns reference to altered object
		public static T ApplySettings <T> (this T _this, T sample) where T: Component
		{
			PropertyInfo[] properties = _this.GetType().GetProperties(defaultBindingFlags);
			foreach (PropertyInfo property in properties)
			{
				ApplyProperty (property, _this, sample);
			}
			return _this;
		}

		//copies the value of one specific property from sample to target object
		private static void ApplyProperty (PropertyInfo property, System.Object target, System.Object sample)
		{
			if (property.GetIndexParameters().Length == 0)
			{
				ApplyPropertyNonIndexed(property, target, sample);
			}
			else 
			{
				ApplyPropertyIndexed(property, target, sample);	
			}
		}
		private static void ApplyPropertyNonIndexed (PropertyInfo property, System.Object target, System.Object sample)
		{
			property.SetValue(target, property.GetValue(sample));
		}
		private static void ApplyPropertyIndexed (PropertyInfo property, System.Object target, System.Object sample)
		{
			Debug.LogWarning("!! ComponentConfigurer.ApplyPropertyIndexed() unimplemented - property \"" + property.Name + "\" ignored");
		}
	}
}
