/*
Project: Land of Dreams (SGD305)
File: pelicanCompanion.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pelicanCompanion : companionBase
{
	public GameObject player;
	
	void Awake()
	{
		player = GameObject.FindWithTag("Player");
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		canFly = true;
		canInteract = true;
		canJump = false;
		moveSpeed = 4.0f;
		
		if (canFly)
		{
			rb.gravityScale = 0.0f;
		}
	}
	
	void FixedUpdate()
	{
		currPosition = gameObject.transform.position;
		newPosition = currPosition;
		
		if (!levelHandler.isCompanionActive)
		{
			Vector3 relPos = player.transform.position;
			
			if (levelHandler.playerIsFacingRight)
			{
				relPos.x -= 2.0f;
			}
			else
			{
				relPos.x += 2.0f;
			}
			
			if (!levelHandler.playerIsCrouching)
			{
				relPos.y += 2.0f;
			}
			
			newPosition = relPos;
		}
		if (levelHandler.isCompanionActive)
		{
			Move();
		}
        
        if (Input.GetAxis("Horizontal") < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        
		gameObject.transform.position = newPosition;
		lastPosition = currPosition;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Ground")
		{
			gameObject.transform.position = lastPosition;
		}
		else if (col.gameObject.tag == "HazardNode")
		{
			Destroy(col.gameObject);
		}
	}
}
