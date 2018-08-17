using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class companionBase : MonoBehaviour
{
	public bool canFly;
	public bool canInteract;
	public bool canJump;
	
	public float moveSpeed;
	public float jumpSpeed;
	public Vector3 currPosition;
	public Vector3 lastPosition;
	public Vector3 newPosition;
	public float jumpDuration;
	float timeForJump;
	public bool isJumping;
	public bool isCrouching;
	Rigidbody2D rb;
	
	void Awake()
	{
		moveSpeed = 1.5f;
		jumpSpeed = 5.0f;
		currPosition = new Vector3(0.0f, 0.0f, 0.0f);
		lastPosition = currPosition;
		jumpDuration = Time.time + 1.0f;
		isJumping = false;
		isCrouching = false;
		rb = GetComponent<Rigidbody2D>();
	}
	
	public void Move()
	{
		newPosition = currPosition;
		
		if (canJump)
		{
			if (Input.GetButtonDown("Jump") && (!isJumping))
			{
				isJumping = true;
				timeForJump = jumpDuration;
			}
		}
		
		newPosition.x += Input.GetAxis("Horizontal") * (moveSpeed * Time.deltaTime);
		
		if (canFly)
		{
			newPosition.y += Input.GetAxis("Vertical") * (moveSpeed * Time.deltaTime);
		}
	}
}
