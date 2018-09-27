using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class verticalEnemyFlag : MonoBehaviour
{
	public int flagID;
	public bool isBottomFlag;
	
	void Start()
	{
		// Purely for debug purposes in the editor, this variable will not be called anywhere else
		isBottomFlag = false;
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if ((col.gameObject.tag == "VerticalEnemy" && (flagID != col.gameObject.GetComponent<verticalEnemy>().flagID)) || (col.gameObject.tag != "VerticalEnemy"))
		{
			Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<Collider2D>());
		}
	}
}
