using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace ASSPhysics.TailSystem
{
	public abstract class TailWiggleElementBase : MonoBehaviour, ITailElement
	{
	
		public float maxOffsetRotation = 30f;		//maximum rotation off from the base value
		public bool baseRotationFromStartingRotation = true;	//wether to fetch rotation from initial state
		protected Quaternion baseRotation;  //base rotation of the element. offsetRotation swings and is clamped around this value

	//Implementación ITailElement
		public float offsetRotation
		{
			get { return _offsetRotation; }
			set { _offsetRotation = Mathf.Clamp(value, -maxOffsetRotation, maxOffsetRotation); }
		}
		private float _offsetRotation = 0.0f;

		public ITailElement[] childElementList
		{
			get 
			{
				if (_childElementList == null) { _childElementList = FetchChildElementList(); }
				return _childElementList;
			}
			private set { _childElementList = value; }
		}
		private ITailElement[] _childElementList;
	//ENDOF Implementación ITailElement

	//MonoBehaviour lifecycle
		public virtual void Awake ()
		{
			childElementList = FetchChildElementList();
		}

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