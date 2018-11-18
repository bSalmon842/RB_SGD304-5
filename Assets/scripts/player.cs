/*
Project: Land of Dreams (SGD305)
File: player.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int health;
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
    public bool isMoving;
    public bool isPaused;
	Rigidbody2D rb;
	public GameObject weapon;
	public GameObject[] cameras;
	public GameObject companion;
    
    public Sprite standSprite;
    Animator animator;
    
	void Start()
	{
        jumpDuration = 0;
        attackDuration = 0;
		weapon = GameObject.FindWithTag("Weapon");
		weapon.SetActive(false);
		
        health = 3;
		moveSpeed = 5.0f;
		jumpSpeed = 4.0f;
		currPosition = new Vector3(0.0f, 0.0f, 0.0f);
		lastPosition = currPosition;
		
        jumpDuration = 1.0f;
		attackDuration = 0.5f;
        
		isJumping = false;
		isCrouching = false;
		isAttacking = false;
        isPaused = false;
		rb = GetComponent<Rigidbody2D>();
		
		companion = GameObject.FindWithTag("Companion");
		
		cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        
        if (cameras[0].transform.parent.gameObject != companion)
        {
            GameObject temp = cameras[0];
            cameras[0] = cameras[1];
            cameras[1] = temp;
        }
        
        cameras[0].SetActive(false);
		cameras[0].GetComponent<AudioListener>().enabled = false;
		cameras[1].SetActive(true);
        
        animator = GetComponent<Animator>();
    }
	
	void FixedUpdate()
	{
        currPosition = gameObject.transform.position;
		Vector3 newPosition = currPosition;
		
		Physics2D.IgnoreCollision(weapon.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		
		if (health <= 0)
		{
			Destroy(gameObject);
		}
		
		if (Input.GetButtonDown("Companion Switch") && (!levelHandler.isCompanionUnavailable))
		{
			levelHandler.isCompanionActive = !levelHandler.isCompanionActive;
			
			if (levelHandler.isCompanionActive)
			{
				// Change to Companion Camera
				cameras[0].SetActive(true);
                cameras[1].GetComponent<AudioListener>().enabled = false;
                cameras[0].GetComponent<AudioListener>().enabled = true;
				cameras[1].SetActive(false);
			}
			else
			{
				// Change to Player Camera
				Vector3 companionResetPos = new Vector3((gameObject.transform.position.x - 2.0f),
														(gameObject.transform.position.y + 2.0f),
														gameObject.transform.position.z);
				companion.transform.position = companionResetPos;
				cameras[0].GetComponent<parallax>().FixedUpdate();
				cameras[1].SetActive(true);
                cameras[0].GetComponent<AudioListener>().enabled = false;
                cameras[1].GetComponent<AudioListener>().enabled = true;
                cameras[0].SetActive(false);
            }
		}
		
		if (!levelHandler.isCompanionActive)
		{
            if (isCrouching)
            {
                moveSpeed = 2.5f;
            }
            else
            {
                moveSpeed = 5.0f;
            }
            
			if (Input.GetButtonDown("Jump") && (!isJumping))
			{
				isJumping = true;
				timeForJump = jumpDuration;
                animator.SetBool("boy_jump", true);
			}
			
			if (Input.GetAxis("Horizontal") > 0)
			{
				levelHandler.playerIsFacingRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
                if (!isJumping && !isCrouching)
                {
                    animator.SetBool("boy_run", true);
                }
                else
                {
                    animator.SetBool("boy_run", false);
                }
                
                
                if (isCrouching)
                {
                    animator.SetBool("boy_crouch", false);
                    animator.SetBool("boy_crouch_walk", true);
                }
                
                isMoving = true;
            }
			else if (Input.GetAxis("Horizontal") < 0)
			{
				levelHandler.playerIsFacingRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
                if (!isJumping && !isCrouching)
                {
                    animator.SetBool("boy_run", true);
                }
                else
                {
                    animator.SetBool("boy_run", false);
                }
                
                if (isCrouching)
                {
                    animator.SetBool("boy_crouch", false);
                    animator.SetBool("boy_crouch_walk", true);
                }
                
                isMoving = true;
			}
            else if (Input.GetAxis("Horizontal") == 0 && !isAttacking)
            {
                if (isCrouching)
                {
                    animator.SetBool("boy_crouch_walk", false);
                }
                
                animator.SetBool("boy_run", false);
                GetComponent<SpriteRenderer>().sprite = standSprite;
                isMoving = false;
            }
            
            newPosition.x += Input.GetAxis("Horizontal") * (moveSpeed * Time.deltaTime);
            
            if (Input.GetButtonDown("Crouch") && (!isCrouching))
            {
                Vector2 crouchSize = new Vector2(3.0f, 4.0f);
                GetComponent<BoxCollider2D>().size = crouchSize;
                isCrouching = true;
                
                if (!isMoving)
                {
                    animator.SetBool("boy_crouch", true);
                }
                else
                {
                    animator.SetBool("boy_crouch", false);
                }
            }
            if (Input.GetButtonUp("Crouch") && (isCrouching))
            {
                Vector2 standSize = new Vector2(3.0f, 8.0f);
                GetComponent<BoxCollider2D>().size = standSize;
                isCrouching = false;
                animator.SetBool("boy_crouch_walk", false);
                animator.SetBool("boy_crouch", false);
            }
            
            if (Input.GetButtonDown("Attack") && !isMoving && !isJumping)
            {
                animator.SetBool("boy_attack", true);
                weapon.SetActive(true);
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
        if ((timeForJump < -1.0f) && ((int)currPosition.y == (int)lastPosition.y))
        {
            timeForJump = 0.0f;
            isJumping = false;
            rb.gravityScale = 2.0f;
            animator.SetBool("boy_jump", false);
        }
        
        if (isAttacking)
        {
            timeForAttack -= Time.deltaTime;
            
            Vector3 weaponSpawnPos = gameObject.transform.position;
            
            if (levelHandler.playerIsFacingRight)
            {
                weaponSpawnPos.x += 0.45f;
                weaponSpawnPos.y += 0.1f;
            }
            else
            {
                weaponSpawnPos.x -= 0.5f;
            }
            
            weaponSpawnPos.y += 0.25f;
            
            weapon.transform.position = weaponSpawnPos;
            
            if (timeForAttack < 0.0f)
            {
                weapon.gameObject.SetActive(false);
                isAttacking = false;
                timeForAttack = 0.0f;
                animator.SetBool("boy_attack", false);
            }
        }
        
        gameObject.transform.position = newPosition;
        lastPosition = currPosition;
        
        if (isCrouching)
        {
            levelHandler.playerIsCrouching = true;
        }
        else
        {
            levelHandler.playerIsCrouching = false;
        }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground" || col.gameObject.tag == "NightGround")
        {
            gameObject.transform.position = lastPosition;
        }
        /*else if (col.gameObject.tag == "Weapon")
        {
        Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
        }*/
    }
}
