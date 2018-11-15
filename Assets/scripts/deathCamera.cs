/*
Project: Land of Dreams (SGD305)
File: deathCamera.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathCamera : MonoBehaviour
{
	public GameObject player;
    public GameObject deathScreen;
	
	void Awake()
	{
		player = GameObject.FindWithTag("Player");
        deathScreen = GameObject.FindWithTag("DeathScreen");
        deathScreen.SetActive(false);
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
        else
        {
            deathScreen.SetActive(true);
        }
        
        if (Input.GetButtonDown("Restart"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
}
