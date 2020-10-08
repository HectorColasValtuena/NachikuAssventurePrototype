using UnityEngine;
using ASSPhysics.Constants; //AnimationNames

//on awake, sets the animator horizontal flip to true or false, alternatively
[RequireComponent(typeof(Animator))]
public class ToolFlip : MonoBehaviour
{
//static space
	private static int flipCounter;
	//returns false, then true, then false, then true...
	private static bool Flip ()
	{
		return (flipCounter++) % 2 > 0;
	}
//ENDOF static space

//instance implementation
	private Animator animator;
	public void Awake ()
	{
		animator = GetComponent<Animator>();
	} 

	public void Start ()
	{
		bool flipVal = Flip(); Debug.Log(flipVal); animator.SetBool(AnimationNames.Tool.horizontalFlip, flipVal);
		//animator.SetBool(AnimationNames.horizontalFlip, Flip());
		Destroy(this);
	}
//ENDOF instance implementation
}
