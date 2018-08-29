using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	public float moveSpeed;
	public float jumpSpeed;
	public Vector3 currPosition;
	public Vector3 lastPosition;
	public float jumpDuration;
	public float attackDuration;
	float timeForJump;
	float timeForAttack;
	public bool isJumping;
	public bool isCrouching;
	public bool isAttacking;
	Rigidbody2D rb;
	public Transform weapon;
	
	void Awake()
	{
		weapon = gameObject.transform.GetChild(0);
		weapon.gameObject.SetActive(false);
		
		moveSpeed = 2.5f;
		jumpSpeed = 4.0f;
		currPosition = new Vector3(0.0f, 0.0f, 0.0f);
		lastPosition = currPosition;
		jumpDuration = Time.time + 1.0f;
		attackDuration = Time.time + 1.0f;
		isJumping = false;
		isCrouching = false;
		isAttacking = false;
		rb = GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate()
	{
		currPosition = gameObject.transform.position;
		Vector3 newPosition = currPosition;
		
		if (Input.GetButtonDown("Companion Switch") && (!levelHandler.isCompanionUnavailable))
		{
			levelHandler.isCompanionActive = !levelHandler.isCompanionActive;
		}
		
		if (!levelHandler.isCompanionActive)
		{
			if (Input.GetButtonDown("Jump") && (!isJumping))
			{
				isJumping = true;
				timeForJump = jumpDuration;
			}
			
			newPosition.x += Input.GetAxis("Horizontal") * (moveSpeed * Time.deltaTime);
			
			if (Input.GetButtonDown("Crouch") && (!isCrouching))
			{
				Vector3 vec = gameObject.transform.localScale;
				vec.y *= 0.5f;
				gameObject.transform.localScale = vec;
				isCrouching = true;
			}
			if (Input.GetButtonUp("Crouch") && (isCrouching))
			{
				Vector3 vec = gameObject.transform.localScale;
				vec.y *= 2.0f;
				gameObject.transform.localScale = vec;
				isCrouching = false;
			}
			
			if (Input.GetButtonDown("Attack"))
			{
				weapon.gameObject.SetActive(true);
				isAttacking = true;
				timeForAttack = attackDuration;
			}
		}
		
		// TODO(bSalmon): Edit Jump code to prevent flight bug
		if (isJumping)
		{
			rb.gravityScale = 0.0f;
			newPosition.y += (jumpSpeed * Time.deltaTime) * timeForJump;
			timeForJump -= Time.deltaTime;
		}
		
		// Reset timeOfJump for next jump in case it went below 0
		if ((timeForJump < 0.0f) && (currPosition.y == lastPosition.y))
		{
			timeForJump = 0.0f;
			isJumping = false;
			rb.gravityScale = 1.0f;
		}
		
		if (isAttacking)
		{
			timeForAttack -= Time.deltaTime;
			
			if (timeForAttack < 0.0f)
			{
				weapon.gameObject.SetActive(false);
				isAttacking = false;
				timeForAttack = 0.0f;
			}
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
	}
}
