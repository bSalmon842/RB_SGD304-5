using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalEnemy : MonoBehaviour
{
	// NOTE(bSalmon): false = down, true = up
	public bool moveDirIsUp;
	public float moveSpeed;
	public Vector3 currPosition;
	public Vector3 lastPosition;
	Rigidbody2D rb;
	
	public int flagID;
	
	void Awake()
	{
		moveDirIsUp = false;
		moveSpeed = 1.0f;
		currPosition = new Vector3(0.0f, 0.0f, 0.0f);
		lastPosition = currPosition;
		rb = GetComponent<Rigidbody2D>();
		
		levelHandler.currentEnemies += 1;
	}
	
	void FixedUpdate()
	{
		currPosition = gameObject.transform.position;
		Vector3 newPosition = currPosition;
		
		if (moveDirIsUp)
		{
			// Move Up
			newPosition.y -= (moveSpeed * Time.deltaTime);
		}
		else
		{
			// Move Down
			newPosition.y += (moveSpeed * Time.deltaTime);
		}
		
		gameObject.transform.position = newPosition;
		lastPosition = currPosition;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "VerticalEnemyFlag")
		{
			if(col.gameObject.GetComponent<verticalEnemyFlag>().flagID == flagID)
			{
				moveDirIsUp = !moveDirIsUp;
			}
		}
		else if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<player>().health--;
		}
		else
		{
			Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), gameObject.GetComponent<Collider2D>());
		}		
	}
	
	void OnDestroy()
	{
		levelHandler.currentEnemies -= 1;
	}
}
