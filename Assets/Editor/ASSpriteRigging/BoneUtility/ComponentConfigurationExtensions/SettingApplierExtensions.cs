using UnityEngine;

namespace ASSpriteRigging.BoneUtility.ComponentConfigurers
{
	//applies right-hand properties to left-hand objects
	public static class SettingApplierExtensions
	{
	//Rigidbody2D components configuration
		public static void ApplySettings (this Rigidbody2D _this, Rigidbody2D sample)
		{
			_this.bodyType = 				sample.bodyType;				//body type
			_this.sharedMaterial = 			sample.sharedMaterial;			//material
			_this.simulated = 				sample.simulated;				//simulated
			_this.mass = 					sample.mass;					//mass
			_this.useAutoMass = 			sample.useAutoMass;				//use auto mass
			_this.drag = 					sample.drag;					//linear drag
			_this.angularDrag = 			sample.angularDrag;				//angular drag
			_this.gravityScale = 			sample.gravityScale;			//gravity scale
			_this.collisionDetectionMode =	sample.collisionDetectionMode;	//collision detection
			_this.sleepMode = 				sample.sleepMode;				//sleeping mode
			_this.interpolation = 			sample.interpolation;			//interpolate
			_this.constraints = 			sample.constraints;				//constraints (freeze position X, Y, freeze rotation Z)
		}
	//ENDOF Rigidbody2D components configuration

	//SpringJoint2D components configuration
		public static void ApplySettings (this SpringJoint2D _this, SpringJoint2D sample, bool alterConnectedBody = false)
		{
			if (alterConnectedBody)	{ _this.connectedBody = sample.connectedBody; }		//connected rigidbody
			_this.enableCollision = 				sample.enableCollision;					//enable collision
			_this.autoConfigureConnectedAnchor = 	sample.autoConfigureConnectedAnchor;	//auto configure connection
			_this.anchor = 							sample.anchor;							//anchor x y
			_this.connectedAnchor = 				sample.connectedAnchor;					//connected anchor x y
			_this.autoConfigureDistance = 			sample.autoConfigureDistance;			//auto configure distance
			_this.distance = 						sample.distance;						//distance
			_this.dampingRatio = 					sample.dampingRatio;					//Damping ratio
			_this.frequency = 						sample.frequency;						//frequency
			_this.breakForce = 						sample.breakForce;						//break force
		}
	//ENDOF SpringJoint2D components configuration

	//Collider2D components configuration
		public static void ApplySettings (this Collider2D _this, Collider2D sample)
		{
			_this.sharedMaterial = 	sample.sharedMaterial;			//material
			_this.isTrigger = 		sample.isTrigger;				//is trigger
			_this.usedByEffector = 	sample.usedByEffector;			//used by an effector or not
			_this.offset = 			sample.offset;					//mass
		}
		public static void ApplySettings (this CircleCollider2D _this, CircleCollider2D sample)
		{
			((Collider2D) _this).ApplySettings((Collider2D) sample);
			_this.radius = 			sample.radius;				//object radius
		}
	//ENDOF Collider2D components configuration
	}
}
