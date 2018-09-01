using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCamera : MonoBehaviour
{
	public GameObject player;
	
	void Awake()
	{
		player = GameObject.FindWithTag("Player");
	}
	
	void Update()
	{
		if (player)
		{
			Vector3 newPosition = player.transform.position;
			newPosition.x += 3.3f;
			newPosition.y += 1.0f;
			newPosition.z -= 10.0f;
			gameObject.transform.position = newPosition;
		}
	}
}
