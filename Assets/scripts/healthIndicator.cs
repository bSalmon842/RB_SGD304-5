/*
Project: Land of Dreams (SGD305)
File: healthIndicator.cs
Author: Brock Salmon
Notice: (C) Copyright 2018 by Brock Salmon. All Rights Reserved.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthIndicator : MonoBehaviour
{
    public int healthIcon;
    public GameObject playerObject;
    
    void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");
    }
    
	void Update ()
    {
		if (playerObject.GetComponent<player>().health < healthIcon)
        {
            Destroy(gameObject);
        }
	}
}
