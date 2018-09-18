using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level2 : MonoBehaviour
{
	public GameObject[] nightGrounds;
	
	void Awake()
	{
		
		nightGrounds = GameObject.FindGameObjectsWithTag("NightGround");
		
		if (nightGrounds != null)
		{
			foreach(GameObject i in nightGrounds)
			{
				i.GetComponent<SpriteRenderer>().enabled = false;
			}
		}
	}
}
