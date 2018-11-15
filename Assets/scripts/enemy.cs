/*
Project: Land of Dreams (SGD305)
File: enemy.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
	// NOTE(bSalmon): false = left, true = right
	public bool moveDirIsRight;
	public float moveSpeed;
	public float jumpSpeed;
	public Vector3 currPosition;
	public Vector3 lastPosition;
	float jumpDuration;
	public float timeForJump;
	public bool isJumping;
	Rigidbody2D rb;
	
	public int flagID;
	
	void Awake()
	{
		moveDirIsRight = false;
		moveSpeed = 1.0f;
		jumpSpeed = 5.0f;
		currPosition = new Vector3(0.0f, 0.0f, 0.0f);
		lastPosition = currPosition;
		jumpDuration = Time.time + 1.0f;
		isJumping = false;
		rb = GetComponent<Rigidbody2D>();
		
		levelHandler.currentEnemies += 1;
	}
	
	void FixedUpdate()
	{
		currPosition = gameObject.transform.position;
		Vector3 newPosition = currPosition;
		
		if (currPosition == lastPosition)
		{
			isJumping = true;
			timeForJump = jumpDuration;
		}
		
		if (isJumping)
		{
			rb.gravityScale = 0.0f;
			newPosition.y += (jumpSpeed * Time.deltaTime) * timeForJump;
			timeForJump -= Time.deltaTime;
		}
		
		if (moveDirIsRight)
		{
			// Move Right
			newPosition.x += (moveSpeed * Time.deltaTime);
		}
		else
		{
			// Move Left
			newPosition.x -= (moveSpeed * Time.deltaTime);
		}
		
		// Reset timeOfJump for next jump in case it went below 0
		if (timeForJump < 0.0f)
		{
			timeForJump = 0.0f;
			isJumping = false;
			rb.gravityScale = 1.0f;
		}
		
		gameObject.transform.position = newPosition;
		lastPosition = currPosition;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "EnemyFlag")
		{
			if(col.gameObject.GetComponent<enemyFlag>().flagID == flagID)
			{
				moveDirIsRight = !moveDirIsRight;
			}
		}
		else if (col.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}
		else if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<player>().health--;
		}
	}
	
	void OnDestroy()
	{
		levelHandler.currentEnemies -= 1;
	}
}
