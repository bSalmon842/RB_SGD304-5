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
		
		backgroundParallaxCoef = 0.5f;
		foregroundParallaxCoef = 0.25f;
		
		currPosition = gameObject.transform.position;
		newPosition = currPosition;
	}
	
	void FixedUpdate()
	{
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