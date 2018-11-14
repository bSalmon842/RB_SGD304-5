using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killHazard : MonoBehaviour {
	
	void Update ()
	{
		if (levelHandler.nodesRemaining == 0)
		{
			Destroy(gameObject);
		}
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Destroy(col.gameObject);
			Destroy(GameObject.FindWithTag("Companion"));
		}
	}
}
