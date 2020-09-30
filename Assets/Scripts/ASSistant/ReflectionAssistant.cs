using System.Reflection;

namespace ASSistant
{
	//Introspection support methods
	public static class ReflectionAssistant
	{
		//Finds a method of name methodName within provided type using provided binding flags
		//invoke it using given context, passing given list of generic types and parameters
		public static void CallMethodWithTypesAndParameters (
			System.Type type,			//type containing target method
			System.Object context,		//context within invokation will occur. Null means static context
			string methodName,			//name of the method
			BindingFlags bindingFlags, 	//binding flags delimiting method search
			System.Type[] typeList,		//list of generic types to pass
			System.Object[] parameters  //list of parameters to pass
		) {
			type
				.GetMethod(methodName, bindingFlags)
				.MakeGenericMethod(typeList)
				.Invoke(context, parameters);
		}
	}
}