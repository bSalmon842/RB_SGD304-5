/*
Project: Land of Dreams (SGD305)
File: killHazard.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killHazard : MonoBehaviour {
	
	void Update ()
	{
		if (levelHandler.nodesRemaining == 0)
		{
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Destroy(col.gameObject);
			Destroy(GameObject.FindWithTag("Companion"));
		}
	}
}
