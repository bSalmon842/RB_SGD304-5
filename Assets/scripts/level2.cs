/*
Project: Land of Dreams (SGD305)
File: level2.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2 : MonoBehaviour
{
	public GameObject[] nightGrounds;
	
	void Awake()
	{
		
		nightGrounds = GameObject.FindGameObjectsWithTag("NightGround");
		
		if (nightGrounds != null)
		{
			foreach(GameObject i in nightGrounds)
			{
				i.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
	}
}
