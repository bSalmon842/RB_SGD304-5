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
	Rigidbody2D rb;
	public GameObject weapon;
	public GameObject[] cameras;
	public GameObject companion;
    
    Animator animator;
    
	void Awake()
	{
		weapon = GameObject.FindWithTag("Weapon");
		weapon.SetActive(false);
		
		health = 3;
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
		
		companion = GameObject.FindWithTag("Companion");
		
		cameras = GameObject.FindGameObjectsWithTag("MainCamera");
        
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
			if (Input.GetButtonDown("Jump") && (!isJumping))
			{
				isJumping = true;
				timeForJump = jumpDuration;
                animator.Play("boy_jump");
			}
			
			if (Input.GetAxis("Horizontal") > 0)
			{
				levelHandler.playerIsFacingRight = true;
                GetComponent<SpriteRenderer>().flipX = false;
            }
			else if (Input.GetAxis("Horizontal") < 0)
			{
				levelHandler.playerIsFacingRight = false;
                GetComponent<SpriteRenderer>().flipX = true;
			}
			
			newPosition.x += Input.GetAxis("Horizontal") * (moveSpeed * Time.deltaTime);
			
			if (Input.GetButtonDown("Crouch") && (!isCrouching))
			{
                Vector2 crouchSize = new Vector2(3.0f, 4.0f);
                GetComponent<BoxCollider2D>().size = crouchSize;
                isCrouching = true;
                animator.Play("boy_crouch");
            }
			if (Input.GetButtonUp("Crouch") && (isCrouching))
			{
                Vector2 standSize = new Vector2(3.0f, 8.0f);
				GetComponent<BoxCollider2D>().size = standSize;
                isCrouching = false;
                animator.Play("boy_crouch_stand");
			}
			
			if (Input.GetButtonDown("Attack"))
			{
				weapon.SetActive(true);
				isAttacking = true;
				timeForAttack = attackDuration;
                animator.Play("boy_attack");
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
		if ((timeForJump < -1.0f) && (currPosition.y == lastPosition.y))
		{
			timeForJump = 0.0f;
			isJumping = false;
			rb.gravityScale = 2.0f;
            animator.Play("boy_jump_stand");
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
                animator.Play("boy_attack_stand");
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
