using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectTransformRectLogger : MonoBehaviour
{
	private RectTransform rectTransform;

	// Start is called before the first frame update
	void Start()
	{
		rectTransform = transform as RectTransform;	
	}

	// Update is called once per frame
	void Update()
	{
		Debug.Log(rectTransform.rect);
	}
}
