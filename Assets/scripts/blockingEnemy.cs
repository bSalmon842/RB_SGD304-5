using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockingEnemy : MonoBehaviour
{
	Rigidbody2D rb;
	
	public int health;
	
	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		
		health = 3;
		
		levelHandler.currentEnemies += 1;
	}
	
	void FixedUpdate()
	{
		if (health <= 0)
		{
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<player>().health--;
		}
		else if (col.gameObject.tag == "Weapon")
		{
			health--;
		}
	}
	
	void OnDestroy()
	{
		levelHandler.currentEnemies -= 1;
	}
}
