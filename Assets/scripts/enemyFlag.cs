using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFlag : MonoBehaviour
{
	public int flagID;
	public bool isLeftFlag;
	
	void Start()
	{
		// Purely for debug purposes in the editor, this variable will not be called anywhere else
		isLeftFlag = false;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if ((col.gameObject.tag == "Enemy" && (flagID != col.gameObject.GetComponent<enemy>().flagID)) || (col.gameObject.tag != "Enemy"))
		{
			Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<Collider2D>());
		}
	}
}
