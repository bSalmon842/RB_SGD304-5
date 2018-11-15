/*
Project: Land of Dreams (SGD305)
File: parallax.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
	public GameObject[] backgroundObjects;
	public GameObject[] foregroundObjects;
	public Vector3 currPosition;
	public Vector3 newPosition;
	public float backgroundParallaxCoef;
	public float foregroundParallaxCoef;
	
	void Awake()
	{
		backgroundObjects = GameObject.FindGameObjectsWithTag("Background");
		foregroundObjects = GameObject.FindGameObjectsWithTag("Foreground");
		
		backgroundParallaxCoef = 0.02f;
		foregroundParallaxCoef = 0.25f;
		
		currPosition = gameObject.transform.position;
		newPosition = currPosition;
	}
	
	public void FixedUpdate()
	{
		// TODO(bSalmon): Fix Companion Switch Parallax Bug
		newPosition = gameObject.transform.position;
		
		if (newPosition.x != currPosition.x)
		{
			float changeInAxis = newPosition.x - currPosition.x;
			float defaultScrollMagnitude = changeInAxis *= -1.0f;
			float backScrollMagnitude = defaultScrollMagnitude * backgroundParallaxCoef;
			float foreScrollMagnitude = defaultScrollMagnitude * foregroundParallaxCoef;
			
			foreach (GameObject i in backgroundObjects)
			{
				Vector3 newBackgroundPosition = i.transform.position;
				newBackgroundPosition.x += backScrollMagnitude;
				i.transform.position = newBackgroundPosition;
			}
			
			foreach (GameObject i in foregroundObjects)
			{
				Vector3 newForegroundPosition = i.transform.position;
				newForegroundPosition.x += foreScrollMagnitude;
				i.transform.position = newForegroundPosition;
			}
		}
		
		currPosition = newPosition;
	}
}