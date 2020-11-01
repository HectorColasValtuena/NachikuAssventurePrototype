using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ASSPhysics.TailSystem
{
	public abstract class TailElementBase : MonoBehaviour, ITailElement
	{
	
		public float maxOffsetRotation = 30f;		//maximum rotation off from the base value
		public bool baseRotationFromStartingRotation = true;	//wether to fetch rotation from initial state
		protected Quaternion baseRotation;  //base rotation of the element. offsetRotation swings and is clamped around this value

	//Implementación ITailElement
		//angle from baseRotation we want to achieve for this element
		public float offsetRotation
		{
			get { return _offsetRotation; }
			set { _offsetRotation = Mathf.Clamp(value, -maxOffsetRotation, maxOffsetRotation); }
		}
		private float _offsetRotation = 0.0f;

		//gets the next element in sequence
		public ITailElement childElement
		{
			get 
			{
				if (_childElement == null) { _childElement = FetchChildElement(); }
				return _childElement;
			}
			private set { _childElement = value; }
		}
		private ITailElement _childElement;
	//ENDOF Implementación ITailElement

	//MonoBehaviour lifecycle
		/*public virtual void Awake ()
		{
			childElement = FetchChildElement();
		}*/

		public virtual void Start ()
		{
			baseRotation = baseRotationFromStartingRotation ? transform.rotation : Quaternion.identity;
		}

		public virtual void Update()
		{
			MatchRotation();
		}
	//ENDOF MonoBehaviour lifecycle

	//internal methods implementation
		//find and return the first child element controller
		private ITailElement FetchChildElement ()
		{
			for (int i = 0, iLimit = transform.childCount; i < iLimit; i++)
			{
				ITailElement element = transform.GetChild(i).GetComponent<ITailElement>();
				if (element != null) { return element; }
			}
			return null;
		}

		private ITailElement[] FetchChildElementList ()
		{
			List<ITailElement> elementList = new List<ITailElement>();

			for (int i = 0, iLimit = transform.childCount; i < iLimit; i++)
			{
				ITailElement element = transform.GetChild(i).GetComponent<ITailElement>();
				if (element != null) { elementList.Add(element); }
			}
			return elementList.ToArray();
		}
	//ENDOF internal methods

	//Overridable methods
		//attempts to match current rotation with target rotation
		protected abstract void MatchRotation ();
	//ENDOF Overridable methods
	}
}