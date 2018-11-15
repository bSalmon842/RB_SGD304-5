/*
Project: Land of Dreams (SGD305)
File: batCompanion.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batCompanion : companionBase
{
	public GameObject player;
	
	public float emitDuration;
	float timeForEmit;
	public bool isEmitting;
	public GameObject sonar;
	public Vector3 resetSize;
	
	void Awake()
	{
		sonar = GameObject.FindWithTag("Sonar");;
		resetSize = sonar.transform.localScale;
		emitDuration = Time.time + 0.3f;
		isEmitting = false;
		sonar.SetActive(false);
		
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
		
		if (timeForEmit <= 0.0f)
		{
			isEmitting = false;
			sonar.transform.localScale = resetSize;
			sonar.SetActive(false);
		}
		
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
		if (levelHandler.isCompanionActive && !isEmitting)
		{
			Move();
			
			if (Input.GetButtonDown("Attack"))
			{
				isEmitting = true;
				timeForEmit = emitDuration;
				sonar.SetActive(true);
			}
		}
		
		if (isEmitting)
		{
			if (timeForEmit > 0.0f)
			{
				Vector3 newSize = sonar.transform.localScale;
				newSize.x *= 1.2f;
				newSize.y *= 1.2f;
				sonar.transform.localScale = newSize;
				
				timeForEmit -= Time.deltaTime;
			}
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
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "NightGround")
		{
			gameObject.transform.position = lastPosition;
		}
		else if (col.gameObject.tag == "HazardNode")
		{
			Destroy(col.gameObject);
		}
	}
}
