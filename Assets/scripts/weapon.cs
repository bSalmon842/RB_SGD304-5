/*
Project: Land of Dreams (SGD305)
File: weapon.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy")
		{
			Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<Collider2D>());
			Destroy(col.gameObject);
		}
	}
}
