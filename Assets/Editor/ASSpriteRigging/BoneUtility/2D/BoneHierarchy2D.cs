using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.U2D.Animation; //SpriteSkin
using U2DAnimationAccessor;	//SpriteSkin.

using ASSpriteRigging.Riggers; //SpriteSkinBaseRigger

namespace ASSpriteRigging.BoneUtility
{
	public static class BoneHierarchy2D
	{
		//finds a joint of type TJoint2D connected to target. returns null if non-existant
		public static TJoint2D BoneFindJoint2DConnected <TJoint2D> (Transform bone, Transform target)
			where TJoint2D: Joint2D
		{
			return BoneFindJoint2DConnected<TJoint2D> (bone, target.gameObject.GetComponent<Rigidbody2D>());
		}
		public static TJoint2D BoneFindJoint2DConnected <TJoint2D> (Transform bone, Rigidbody2D targetRigidbody)
			where TJoint2D: Joint2D
		{
			//get a list of all the joints of type TJoint2D contained in the origin bone
			TJoint2D[] jointList = bone.gameObject.GetComponents<TJoint2D>();
			//find a joint connected to target rigidbody and return it
			foreach (TJoint2D joint in jointList)
			{
				if (joint.connectedBody == targetRigidbody)
				{
					return joint;
				}
			}
			return null;//return null if none found
		}
	}
}