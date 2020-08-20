using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpriteRigging.BoneUtility
{
	public static class BoneNomenclature
	{
		//CAREFUL!!!
		//Currently, an anchor name parameter containing tag characters would be recognized as said tag. avoid capitalizing those characters.
		private const char tagBodySeparator = '$';
		private const char rigidbodyTag = 'R';
		private const char anchorTag = 'A';
		private const char springTag = 'S';
		private const char ignoreTag = 'I';
		private const char parameterBeginTag = '(';
		private const char parameterCloseTag = ')';

		//trims the proper name of the target object or full name
		public static string GetProperName (Transform target) { return GetProperName(target.name); }
		public static string GetProperName (string name)
		{
			return name.Split(tagBodySeparator)[0];
		}

		//trims the paramaters of the target object or full name
		public static string GetParameterList (Transform target) { return GetParameterList(target.name); }
		public static string GetParameterList (string name)
		{
			if (!name.Contains(tagBodySeparator.ToString())) { return name; }
			return name.Split(tagBodySeparator)[1];
		}

		public static bool IsIgnored (Transform target) { return IsIgnored(target.name); }
		public static bool IsIgnored (string name)
		{
			//returns true if contains ignored tag or no tag body at all
			return !name.Contains(tagBodySeparator.ToString()) || GetParameterList(name).Contains(ignoreTag.ToString());
		}

		//true if the target requires a rigidbody
		public static bool RequiresRigidbody (Transform target) { return RequiresRigidbody(target.name); }
		public static bool RequiresRigidbody (string name)
		{
			return GetParameterList(name).Contains(rigidbodyTag.ToString());
		}

		//true if the target requires a ground anchor towards its parent
		public static bool RequiresAnchor (Transform target) { return RequiresAnchor(target.name); }
		public static bool RequiresAnchor (string name)
		{
			return GetParameterList(name).Contains(anchorTag.ToString());
		}

		//get a list of the proper names target bone requires a spring towards
		public static string[] GetSpringTargets (Transform target) { return GetSpringTargets(target.name); }
		public static string[] GetSpringTargets (string name)
		{
			List<string> targetNames = new List<string>();
			int tagFoundIndex = -1;

			//find the next instance of springTag within the string. execute until none found (-1)
			while ((tagFoundIndex = name.IndexOf(springTag, tagFoundIndex + 1)) >= 0)
			{
				string boneName = BoneNomenclature.FindParameter(name, tagFoundIndex);
				if (boneName != null) { targetNames.Add(boneName); }
			}

			return targetNames.ToArray();
		}

		public static Transform[] FindBonesByProperName (string[] nameList, Transform targetRoot, bool recursive = true)
		{
			//=================================================================================================================
			//[TO-DO]
			//=================================================================================================================
			return null;
		}

		//find the next parameter between brackets
		private static string FindParameter (string name, int tagStartIndex)
		{
				int parameterStartIndex = name.IndexOf(parameterBeginTag, tagStartIndex);
				//return empty if no opening bracket found
				if (parameterStartIndex < 0) { return ""; }
				int parameterEndIndex = name.IndexOf(parameterCloseTag, parameterStartIndex);
				//return slice from opening bracket to the closing bracket or the end of the string
				if (parameterEndIndex < 0) { return name.Substring(parameterStartIndex + 1); }
				int parameterLength = parameterEndIndex - parameterStartIndex - 1;
				if (parameterLength < 0) { return ""; }
				return name.Substring(parameterStartIndex + 1, parameterLength);
		}
	}
}