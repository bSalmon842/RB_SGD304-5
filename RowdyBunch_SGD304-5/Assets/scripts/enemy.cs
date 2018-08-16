﻿using System.Collections;
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

	void Start()
	{
		moveDirIsRight = false;
		moveSpeed = 1.0f;
		jumpSpeed = 5.0f;
		currPosition = new Vector3(0.0f, 0.0f, 0.0f);
		lastPosition = currPosition;
		jumpDuration = Time.time + 1.0f;
		isJumping = false;
		rb = GetComponent<Rigidbody2D>();
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
			enemyFlag flag = col.gameObject.GetComponent<enemyFlag>();
			if(flag.flagID != flagID)
			{
				Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<Collider2D>());
			}
			else
			{
				moveDirIsRight = !moveDirIsRight;
			}
		}
		else if (col.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}		
	}
}