using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFlag : MonoBehaviour
{
	public int flagID;
	public bool isLeftFlag;

	void Start()
	{
		// Purely for debug purposes in the editor, this variable will not be called anywhere else
		isLeftFlag = false;
	}
}
