using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
	public void ConsoleLog()
	{
		Debug.Log("L M A O");
	}

	public void CustomLog(string message)
	{
		Debug.Log(message);
	}
}
