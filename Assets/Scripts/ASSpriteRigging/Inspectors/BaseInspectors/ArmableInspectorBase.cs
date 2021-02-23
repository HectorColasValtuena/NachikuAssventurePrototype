using UnityEngine;
using UnityEngine.U2D.Animation;

namespace ASSpriteRigging.Inspectors
{
	public class  ArmableInspectorBase : MonoBehaviour, IArmableInspector
	{
	//IArmableInspector implementation
		[SerializeField]
		private bool _armed = false;
		public bool armed 
		{
			get { return _armed; }
			set { _armed = value; }
		}
	//ENDOF IArmableInspector implementation
	}
}